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
// Modified by: Andrew Warner - 3-20-11
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
                string chat = null;

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
                        else if (innerNodes.Name == "Chat")
                            chat = innerNodes.InnerText;
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
                    if (chat != null)
                        output += " Chat: " + chat;

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
                                else if (childnode.Name == "Chat")
                                {
                                    eventNode.Text = "BotChat: " + childnode.InnerText;
                                    eventNode.ForeColor = System.Drawing.Color.Blue;
                                }

                                eventIDNode.Nodes.Add(eventNode);
                            }
                        }
                        displayAIMLTriggerQuestions(eventListLB.SelectedIndex);
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
            selectedNodeID = eventListLB.SelectedIndex;
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
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["eventID"].Value == eventListLB.SelectedIndex.ToString() && Int32.Parse(node.Attributes["eventID"].Value) != 0)
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

        private string getSelectedBotName()
        {
            return comboBox1.SelectedItem.ToString();
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

        private void Exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            // Launches formEventEditor
            try
            {
                formEventEditor eventWindow = new formEventEditor(getSelectedBotName(), eventListLB.SelectedIndex);
                eventWindow.ShowDialog();
                eventWindow.Dispose();
            }
            // Error catch when no event is selected
            catch (ArgumentOutOfRangeException)
            {
                string message = "Please select an event.";
                string caption = "Event Not Found";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            eventListLB_SelectedIndexChanged(this, new EventArgs());
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            XmlDocument events = new XmlDocument();
            events.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");

            XmlDocument questions = new XmlDocument();
            questions.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");

            // If something is selected, prompt for deletion.
            if (eventListLB.SelectedIndex != -1)
            {
                string message = "Are you sure you want to delete the selected event?";
                string caption = "Remove Event";
                DialogResult remove = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

                if (remove == DialogResult.Yes)
                {
                    // Removes selected event from events.xml.
                    XmlNodeList eventNodes = events.SelectNodes("//event[@ID=" + eventListLB.SelectedIndex + "]");
                    for (int i = eventNodes.Count - 1; i >= 0; i--)
                    {
                        eventNodes[i].ParentNode.RemoveChild(eventNodes[i]);
                    }

                    // Removes selected event's question from questions.xml.
                    XmlNodeList questionNodes = questions.SelectNodes("//Question[@eventID=" + eventListLB.SelectedIndex + "]");
                    for (int i = questionNodes.Count - 1; i >= 0; i--)
                    {
                        questionNodes[i].ParentNode.RemoveChild(questionNodes[i]);
                    }

                    // Reorders all subsequent events in events.xml and all subsequent questions in questions.xml.
                    int x = eventListLB.SelectedIndex;
                    for (int j = x; j < events.DocumentElement.ChildNodes.Count; j++)
                    {
                        eventListLB.SelectedIndex++;
                        XmlNode eventNode = events.SelectSingleNode("//event[@ID=" + eventListLB.SelectedIndex + "]");
                        XmlNode questionNode = questions.SelectSingleNode("//Question[@eventID=" + eventListLB.SelectedIndex + "]");
                        eventNode.Attributes["ID"].Value = (Int32.Parse(eventNode.Attributes["ID"].Value) - 1).ToString();
                        questionNode.Attributes["eventID"].Value = (Int32.Parse(questionNode.Attributes["eventID"].Value) - 1).ToString();
                    }

                    eventListLB.Items.RemoveAt(eventListLB.SelectedIndex);
                    eventListLB.Items.Clear();

                    // Refreshes eventListLB with changes.
                    foreach (XmlNode node in events.DocumentElement.ChildNodes)
                    {
                        String eventID = "Event #" + node.Attributes["ID"].Value + ": " + node.Attributes["Name"].Value;
                        eventListLB.Items.Add(eventID);
                    }
                    events.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");
                    questions.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");
                    eventListLB_SelectedIndexChanged(this, new EventArgs());
                }
                else if (remove == DialogResult.No)
                {
                    // Do nothing.
                }
            }
            else // Error handling if nothing is selected.
            {
                string message = "No event selected. Please select an event from the list.";
                string caption = "Event Not Found";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMoveUp_Click(Object sender, System.EventArgs e)
        {
            XmlDocument events = new XmlDocument();
            events.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");

            XmlDocument questions = new XmlDocument();
            questions.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");

            // If item is selected, item will move up list; else catch ArgumentOutOfRangeException and do nothing.
            try
            {
                int i = this.eventListLB.SelectedIndex;
                int iRef = i - 1;
                object o = this.eventListLB.SelectedItem;

                if (i > 0)
                {
                    XmlNode newEventNode = events.SelectSingleNode("//event[@ID=" + i + "]");
                    XmlNode refEventNode = events.SelectSingleNode("//event[@ID=" + iRef + "]");

                    XmlNode newQuestionNode = questions.SelectSingleNode("//Question[@eventID=" + i + "]");
                    XmlNode refQuestionNode = questions.SelectSingleNode("//Question[@eventID=" + iRef + "]");

                    if (newEventNode != o)
                    {
                        // Swap the events in events.xml and rename event ID number to match.
                        newEventNode.ParentNode.InsertBefore(newEventNode, refEventNode);
                        refEventNode.Attributes["ID"].Value = Int32.Parse(newEventNode.Attributes["ID"].Value).ToString();
                        newEventNode.Attributes["ID"].Value = (Int32.Parse(newEventNode.Attributes["ID"].Value) - 1).ToString();

                        try
                        {
                            // If a reference question does not exist, one will be created to update questions.xml accordingly.
                            if (refQuestionNode == null)
                            {
                                XmlElement addQuestionNode = questions.CreateElement("Question");
                                XmlAttribute addQuestionNodeAtt = questions.CreateAttribute("eventID");
                                var text = newQuestionNode.InnerText;

                                addQuestionNode.SetAttributeNode(addQuestionNodeAtt);
                                addQuestionNode.SetAttribute("eventID", iRef.ToString());
                                addQuestionNode.InnerText = text;

                                questions.DocumentElement.PrependChild(addQuestionNode);
                                newQuestionNode.InnerText = "There are no AIML triggers for this event";
                                newQuestionNode.ParentNode.InsertBefore(refQuestionNode, newQuestionNode);
                            }
                            // Swap the questions in questions.xml and rename event ID number to match.
                            else if (refQuestionNode != null)
                                newQuestionNode.ParentNode.InsertBefore(newQuestionNode, refQuestionNode);

                            refQuestionNode.Attributes["eventID"].Value = Int32.Parse(newQuestionNode.Attributes["eventID"].Value).ToString();
                            newQuestionNode.Attributes["eventID"].Value = (Int32.Parse(newQuestionNode.Attributes["eventID"].Value) - 1).ToString();
                        }
                        catch (NullReferenceException) // Catch exception when no eventID exists.
                        {
                            // Do nothing.
                        }
                    }
                    this.eventListLB.Items.RemoveAt(i);
                    this.eventListLB.Items.Insert(iRef, o);
                }
                eventListLB.Items.Clear();

                foreach (XmlNode node in events.DocumentElement.ChildNodes)
                {
                    String eventID = "Event #" + node.Attributes["ID"].Value + ": " + node.Attributes["Name"].Value;
                    eventListLB.Items.Add(eventID);
                }
                this.eventListLB.SelectedIndex = i - 1;

                events.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");
                questions.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");
                eventListLB_SelectedIndexChanged(o, new EventArgs());
            }
            catch (ArgumentOutOfRangeException)
            {
                // Do nothing.
            }
        }

        private void buttonMoveDown_Click(Object sender, System.EventArgs e)
        {
            XmlDocument events = new XmlDocument();
            events.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");

            XmlDocument questions = new XmlDocument();
            questions.Load(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");

            // If item is selected, item will move down list; else catch ArgumentOutOfRangeException and do nothing.
            try
            {
                int i = this.eventListLB.SelectedIndex;
                int iRef = i + 1;
                object o = this.eventListLB.SelectedItem;

                if (i < this.eventListLB.Items.Count - 1)
                {
                    XmlNode newEventNode = events.SelectSingleNode("//event[@ID=" + i + "]");
                    XmlNode refEventNode = events.SelectSingleNode("//event[@ID=" + iRef + "]");

                    XmlNode newQuestionNode = questions.SelectSingleNode("//Question[@eventID=" + i + "]");
                    XmlNode refQuestionNode = questions.SelectSingleNode("//Question[@eventID=" + iRef + "]");

                    if (newEventNode != o)
                    {
                        // Swap the events in events.xml and rename event ID number to match.
                        newEventNode.ParentNode.InsertAfter(newEventNode, refEventNode);
                        refEventNode.Attributes["ID"].Value = Int32.Parse(newEventNode.Attributes["ID"].Value).ToString();
                        newEventNode.Attributes["ID"].Value = (Int32.Parse(newEventNode.Attributes["ID"].Value) + 1).ToString();

                        try
                        {
                            // If a new question does not exist, one will be created to update questions.xml accordingly.
                            if (newQuestionNode == null)
                            {
                                XmlElement addQuestionNode = questions.CreateElement("Question");
                                XmlAttribute addQuestionNodeAtt = questions.CreateAttribute("eventID");
                                var text = refQuestionNode.InnerText;

                                addQuestionNode.SetAttributeNode(addQuestionNodeAtt);
                                addQuestionNode.SetAttribute("eventID", i.ToString());
                                addQuestionNode.InnerText = text;

                                questions.DocumentElement.PrependChild(addQuestionNode);
                                refQuestionNode.InnerText = "There are no AIML triggers for this event";
                            }
                            // Swap the questions in questions.xml and rename event ID number to match.
                            newQuestionNode.ParentNode.InsertAfter(newQuestionNode, refQuestionNode);
                            refQuestionNode.Attributes["eventID"].Value = Int32.Parse(newQuestionNode.Attributes["eventID"].Value).ToString();
                            newQuestionNode.Attributes["eventID"].Value = (Int32.Parse(newQuestionNode.Attributes["eventID"].Value) + 1).ToString();
                        }
                        catch (NullReferenceException) // Catch exception when no eventID exists.
                        {
                            // Do nothing.
                        }
                    }
                    this.eventListLB.Items.RemoveAt(i);
                    this.eventListLB.Items.Insert(iRef, o);
                }
                eventListLB.Items.Clear();

                foreach (XmlNode node in events.DocumentElement.ChildNodes)
                {
                    String eventID = "Event #" + node.Attributes["ID"].Value + ": " + node.Attributes["Name"].Value;
                    eventListLB.Items.Add(eventID);
                }
                this.eventListLB.SelectedIndex = i + 1;

                events.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\events.xml");
                questions.Save(Application.StartupPath + "\\Bots\\" + getSelectedBotName() + "\\Events\\questions.xml");
                eventListLB_SelectedIndexChanged(o, new EventArgs());
            }
            catch (ArgumentOutOfRangeException)
            {
                // Do nothing.
            }
        }
    }
}