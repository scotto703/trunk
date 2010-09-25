//**************************************************************
// Class: formEventViewer
// 
// Author: Joel McClain
//
// Date: 1-5-10
//
// Description: This form displays the events for a given bot in
// a human readable interface.  It also allows for the creation,
// deletion, and insertion of new events and actions for those 
// events, as well as modifying existing events.               
//
//**************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace BotGUI
{
    public partial class formEventViewer : Form
    {        
        public formEventViewer()
        {
            InitializeComponent();
            GetBotList();
            comboBox1.SelectedIndex = 0;
        }        

        private void GetBotList()
        {
            string BotName;

            //
            //get current directory and add bot list folder to path
            //
            string BotPath = Directory.GetCurrentDirectory() + "\\Bots";

            //
            //gets a list for files in the directory
            //
            DirectoryInfo BotDirectory = new DirectoryInfo(BotPath);
            DirectoryInfo[] BotList = BotDirectory.GetDirectories();

            //
            // Adds the names of each bot to the list of Available bots
            //
            for (int n = 0; n < BotList.Length; n++)
            {
                if (!BotList[n].Attributes.ToString().Contains("Hidden"))
                {
                    BotName = BotList[n].Name;
                    comboBox1.Items.Add(BotName);
                }
            }
        }        

        private string ProcessBotActionNode(XmlNode childnode)
        {
            foreach (XmlNode node in childnode.ChildNodes)
            {
                string UUID = null;
                string invItem = null;
                string attachPT = null;
                string vectorCoord = null;
                string actionType = null;
                string timer = null;
                string sleep = null;

                if (node.Name == "createAction")
                {
                    foreach (XmlNode innerNodes in node.ChildNodes)
                    {
                        //
                        // Set text for Action desired
                        //
                        if (innerNodes.Name == "UUID")
                            UUID = innerNodes.InnerText;
                        else if (innerNodes.Name == "InvItem")
                            invItem = innerNodes.InnerText;
                        else if (innerNodes.Name == "AttachPoint")
                            attachPT = innerNodes.InnerText;
                        else if (innerNodes.Name == "Vector")
                            vectorCoord = GetVectorAsString(innerNodes);
                        else if (innerNodes.Name == "actionType")
                            actionType = processActionType(innerNodes.InnerText);
                        else if (innerNodes.Name == "Timer")
                            timer = innerNodes.InnerText;
                        else if (innerNodes.Name == "SleepTime")
                            sleep = innerNodes.InnerText;
                    }

                    //
                    // create string for action
                    //
                    string output = actionType;

                    if (UUID != null)
                        output += " UUID: " + UUID;
                    if (invItem != null)
                        output += " Inventory Item: " + invItem;
                    if (attachPT != null)
                        output += " Attach: " + attachPT;
                    if (vectorCoord != null)
                        output += " Vector: " + vectorCoord;
                    if (timer != null && actionType == "1")
                        output += " Timer: " + timer;
                    if (sleep != null)
                        output += " " + sleep + " (milliseconds)";

                    //
                    // return the Action as a string
                    //
                    return output;
                }
            }
            return "Error";
        }

        private string processActionType(string p)
        {
            string actionName;

            switch (p)
            {
                case "1":
                    actionName = "Animation";
                    return actionName;
                case "2":
                    actionName = "Sit";
                    return actionName;
                case "3":
                    actionName = "Turn Towards (Look at)";
                    return actionName;
                case "4":
                    actionName = "Stand";
                    return actionName;
                case "5":
                    actionName = "Attach object";
                    return actionName;
                case "6":
                    actionName = "Sleep Bot";
                    return actionName;
                default:
                    return "Error";
            }
        }

        private string GetVectorAsString(XmlNode cnode)
        {
            XmlNodeList vector = cnode.ChildNodes;
            if (cnode.Name == "moveTo" || cnode.Name == "Vector")
                return vector[0].InnerText + "," + vector[1].InnerText + "," + vector[2].InnerText;
            else if (cnode.Name == "Teleport")
            {
                foreach (XmlNode node in vector)
                {
                    if (node.HasChildNodes && node.Name == "Vector")
                    {
                        XmlNodeList list = node.ChildNodes;
                        return list[0].InnerText + "," + list[1].InnerText + "," + list[2].InnerText;
                    }
                }
            }
            return "Error";
        }       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // Clear all views of any previous data
            //
            treeView1.Nodes.Clear();
            eventListLB.Items.Clear();
            questionsLB.Items.Clear();

            //
            // Set foreground color for the list box
            //
            eventListLB.ForeColor = System.Drawing.Color.Black;

            //
            // Load the file and display the events in the list box
            //
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");

                //
                // Check to make sure the root element is not empty
                //
                if (doc.DocumentElement.ChildNodes.Count == 0)
                {
                    Exception noEventsException = new Exception();
                    throw noEventsException;
                }

                //
                // Root element is not empty, so process the file
                //
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    String eventID = "Event #" + node.Attributes["ID"].Value + ": " + node.Attributes["Name"].Value;
                    eventListLB.Items.Add(eventID);
                }

                //
                // Show controls for events
                //
                EventLbl.Visible = true;
                editBtn.Enabled = true;
                removeBtn.Enabled = true;
                addEventBtn.Enabled = true;
            }
            //
            // No events file found, so display the error messages
            //
            catch (Exception)
            {
                //
                // Output when there are no events for chosen bot
                //
                String noEventsText = "There are no events yet for this bot";
                
                //
                // Add the output to the TreeView
                //
                TreeNode noEventNode = new TreeNode();
                noEventNode.Text = noEventsText;
                noEventNode.ForeColor = System.Drawing.Color.Green;
                treeView1.Nodes.Add(noEventNode);

                //
                // Add the output to the List Box
                //
                eventListLB.ForeColor = System.Drawing.Color.Green;
                eventListLB.Items.Add(noEventsText);

                //
                // Sets Event label visibility
                //
                EventLbl.Visible = false;

                //
                // Clear controls when no events are present
                //
                questionsLB.Items.Clear();
                removeBtn.Enabled = false;
                editBtn.Enabled = false;                
            }
        }

        private void eventListLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    int selectedNodeID = eventListLB.SelectedIndex;

                    if (node.Attributes["ID"].Value == selectedNodeID.ToString())
                    {
                        TreeNode selectedNode = new TreeNode("Event #" + node.Attributes["ID"].Value + ":");
                        treeView1.Nodes.Add(selectedNode);

                        TreeNode eventIDNode = new TreeNode(node.Attributes["Name"].Value);
                        selectedNode.Nodes.Add(eventIDNode);

                        if (node.HasChildNodes)
                        {
                            foreach (XmlNode childnode in node.ChildNodes)
                            {
                                TreeNode eventNode = new TreeNode();
                                string output = "";

                                if (childnode.Name == "movement")
                                {
                                    XmlNodeList list = childnode.ChildNodes;
                                    foreach (XmlNode cnode in list)
                                    {
                                        if (cnode.Name == "moveTo")
                                            output = output + "Walk: " + GetVectorAsString(cnode);

                                        else if (cnode.Name == "Teleport")
                                            output = output + "Teleport: " + GetVectorAsString(cnode);
                                    }
                                    eventNode.Text = "BotMove: " + output;
                                    eventNode.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else if (childnode.Name == "action")
                                {
                                    eventNode.Text = "BotAction: " + ProcessBotActionNode(childnode);
                                    eventNode.ForeColor = System.Drawing.Color.Crimson;
                                }
                                else if (childnode.Name == "chat")
                                {
                                    eventNode.Text = "BotChat: " + childnode.InnerText;
                                    eventNode.ForeColor = System.Drawing.Color.Blue;
                                }

                                eventIDNode.Nodes.Add(eventNode);
                            }
                        }
                        displayAIMLTriggerQuestions(selectedNodeID);                        
                    }
                }
            }
            catch (XmlException)
            {
                // Do nothing
            }
        }

        private void displayAIMLTriggerQuestions(int selectedNodeID)
        {
            //
            // Create and load XML document
            //
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");

            //
            // Clear the list box of any previous data
            //
            questionsLB.Items.Clear();

            //
            // Get all the elements with the tag name "Question"
            //
            XmlNodeList nodeList = doc.GetElementsByTagName("Question");
            
            //
            // Loop through questions and compare eventID attribute with selectedNodeID value.
            // If the values are equal, we then add the text to the list box
            //
            foreach(XmlNode node in nodeList)
            {
                if(node.Attributes["eventID"].Value == selectedNodeID.ToString() && Int32.Parse(node.Attributes["eventID"].Value) != 0)
                {
                    questionsLB.Items.Add(node.InnerText);                    
                }
            }
            
            //
            // Check to see if there are no questions for the event, if so display message
            //
            if (questionsLB.Items.Count == 0)
                questionsLB.Items.Add("There are no AIML triggers for this event");
        }

        private void addEventBtn_Click(object sender, EventArgs e)
        {         
            //
            // Create the instance of the window and display it.  Then release
            // resources.
            //
            frmNewEvent eventWindow = new frmNewEvent(getSelectedBotName());
            eventWindow.ShowDialog();
            eventWindow.Dispose();

            //
            // Update the control windows
            //
            comboBox1_SelectedIndexChanged(this, new EventArgs());

        }

        private string getSelectedBotName()
        {           
            return comboBox1.SelectedItem.ToString();
        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
