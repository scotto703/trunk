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
    public partial class frmNewEvent : Form
    {
        string botName;

        public frmNewEvent(string name)
        {
            InitializeComponent();
            botName = name;
        }        

        private void btn_CreateEvent_Click(object sender, EventArgs e)
        {
            if (verifyFields() == true)
            {
                //
                // Gather the data needed to create a new event
                //
                string eventName = tb_EventName.Text;
                string eventAimlQuestion = tb_AimlQuestion.Text;
                int eventNumber = getEventNumber();

                //
                // create the event
                //
                insertEvent(eventNumber, eventName, eventAimlQuestion);
            }
        }

        private void insertEvent(int eventNumber, string eventName, string eventAimlQuestion)
        {
            //
            // Here we call two helper methods. The first one creates the event
            // node in the events.xml file.  The second one creates the question node
            // in the questions.xml file
            //
            CreateNewEventNode(eventNumber, eventName);
            CreateNewQuestionNode(eventAimlQuestion, eventNumber);

            //
            // Output Message and close the form
            //
            MessageBox.Show("Successfully created event \"" + eventName + "\"");
            this.Close();
        }

        private void CreateNewQuestionNode(string eventAimlQuestion, int eventNumber)
        {
            //
            // Create the Xml document object and load the questions.xml file for the bot
            //
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\Bots\\" + botName + "\\Events\\questions.xml");

            //
            // Here we do a little housekeeping.  When a bot is first created, a "dummy" question node 
            // is inserted, which is needed to load BotEventReader, but will never be used. 
            // Because we are now adding a real question node, we can safely delete the "dummy" node if the node
            // still exists.
            //
            if (doc.DocumentElement.ChildNodes.Count == 1)
            {
                XmlNodeList list = doc.GetElementsByTagName("Question");               

                if(list[0].InnerXml == "This is a dummy question/event and should never be called, but is needed to load bots with no movement questions")
                    doc.DocumentElement.RemoveChild(list[0]);                
            }

            //
            // Create the new node that will be inserted,the attribute for the node, and
            // the text that the node will have 
            //
            XmlElement newQuestionNode = doc.CreateElement("Question");
            XmlAttribute newEventNumberAttribute = doc.CreateAttribute("eventID");
            XmlText question = doc.CreateTextNode(eventAimlQuestion);

            //
            // Set the value of the attribute
            //
            newEventNumberAttribute.Value = eventNumber.ToString();

            //
            // Add the attribute to the new Question node
            //
            newQuestionNode.Attributes.Append(newEventNumberAttribute);

            //
            // Add the text to the new Question Node
            //
            newQuestionNode.AppendChild(question);

            //
            // Add the new Question node to the questions.xml file at the end
            // of the root node and save the file.
            //
            doc.DocumentElement.AppendChild(newQuestionNode);
            doc.Save(Application.StartupPath + "\\Bots\\" + botName + "\\Events\\questions.xml");            
        }

        private void CreateNewEventNode(int eventNumber, string eventName)
        {
            //
            // Create the Xml document object and load the events.xml file for the bot
            //
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\Bots\\" + botName + "\\Events\\events.xml");

            //
            // Create the new node that will be inserted and the attribute names for the node
            //
            XmlElement newEventNode = doc.CreateElement("event");
            XmlAttribute newEventNumberAttribute = doc.CreateAttribute("ID");
            XmlAttribute newEventNameAttribute = doc.CreateAttribute("Name");

            //
            // Set the values of each attribute
            //
            newEventNumberAttribute.Value = eventNumber.ToString();
            newEventNameAttribute.Value = eventName;

            //
            // Add the attributes to the new node
            //
            newEventNode.Attributes.Append(newEventNumberAttribute);
            newEventNode.Attributes.Append(newEventNameAttribute);

            //
            // Add the new event node to the end of the events.xml file
            // and save the document
            //
            doc.DocumentElement.AppendChild(newEventNode);
            doc.Save(Application.StartupPath + "\\Bots\\" + botName + "\\Events\\events.xml");
        }

        private bool verifyFields()
        {
            //
            // So far, no errors!
            //
            bool error = false;

            //
            // Loop through each control on the form
            //
            foreach (Control c in this.Controls)
            {
                //
                // find the TextBox controls and check to see if they are blank.
                // If so, flag error as true and set the background of the textbox to yellow
                //
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        c.BackColor = System.Drawing.Color.Yellow;
                        error = true;
                    }
                }
            }

            //
            // display help message if error is true and return false (VerifyFields() failed), otherwise 
            // return true (VerifyFields() passed)
            //
            if (error == true)
            {
                if(tb_AimlQuestion.Text == "" && tb_EventName.Text == "")
                {
                    MessageBox.Show("Please enter a name and an Aiml Question for the event");
                    return false;
                }
                else if (tb_EventName.Text == "")
                {
                    MessageBox.Show("Please enter a name for the new event");
                    return false;
                }
                else if (tb_AimlQuestion.Text == "")
                {
                    MessageBox.Show("Please enter an Aiml Question for your event");
                    return false;
                }
                else
                {
                    MessageBox.Show("Error reading data entered. Fill out the fields and try again.");
                    return false;
                }
            }
            else
                return true;
        }

        private int getEventNumber()
        {
            try
            {
                //
                // Load the events.xml document for the choosen bot and return 
                // the number of <event> nodes 
                // 

                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + "\\bots\\" + botName + "\\Events\\events.xml");
                return doc.DocumentElement.ChildNodes.Count;

            }
            catch (XmlException)
            {
                // *****************************
                // *                           *
                // * Exception caught when the *
                // * Xml file is empty.        *
                // *                           *
                // *****************************

                CreateDefaultXmlDocument();
                return getEventNumber();
            }
            catch (FileNotFoundException)
            {
                CreateDefaultXmlDocument();
                return getEventNumber();
            }
        }

        private void CreateDefaultXmlDocument()
        {
            //
            // Create the Xml document object
            //
            XmlDocument doc = new XmlDocument();

            //
            // Create the Xml declaration and root node for the events.xml file
            //
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
            XmlElement rootNode = doc.CreateElement("events");

            //
            // Add the declartation and root node to the document
            //
            doc.AppendChild(declaration);
            doc.AppendChild(rootNode);

            //
            // Save the file
            //
            doc.Save(Application.StartupPath + "\\bots\\" + botName + "\\Events\\events.xml");
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();            
        }
    }
}
