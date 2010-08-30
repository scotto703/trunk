//******************************************
//*** Refactored By: Joel McClain
//*** Date: 12-14-09
//***
//***Written by Allan Blackford
//***
//***Modified by Justin Hemker
//***Las Modified: 2/6/2009
//******************************************

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BotGUI
{
    public partial class frmCreate : Form
    {
        #region Attributes

        private static string FirstName = "";
        private static string LastName = "";
        private static string Password = "";
        private static string InWorldLocation = "";
        //private static string BotCoordinates = "";
        private static string x;
        private static string y;
        private static string z;
        private static string AIMLpath = "";
        private static string SettingsPath = "";
        private static Boolean Error = false;

        #endregion

        #region Constructor

        public frmCreate()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void VerifyFields()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == "")
                    {
                        c.BackColor = Color.Yellow;
                        Error = true;
                    }
                    else
                        c.BackColor = Color.White;
                }
            }

            // Check to see if a text box was blank
            if (Error == true)
            {
                MessageBox.Show(@"The yellow field(s) cannot be empty", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //coordinate field can only contain integers
            //place coordinates into string array
            string coordinates = txtCoordinates.Text;
            string[] TestCoordinates = (coordinates.Split(new char[] { ',' }));

            //array can must have 3 elements
            if (TestCoordinates.Length != 3)
            {
                MessageBox.Show("Bot Corrdinates must be in the form of 'x,y,z'");
                txtCoordinates.BackColor = Color.Yellow;
                Error = true;
                return;
            }
            else //if there are 3 elements verify they are integers by parsing
            {
                try
                {
                    int x = int.Parse(TestCoordinates[0]);
                    int y = int.Parse(TestCoordinates[1]);
                    int z = int.Parse(TestCoordinates[2]);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Coordinates can only be integers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error = true;
                    return;
                }
            }
            //if everything is ok, set error to false
            Error = false;
        }
        private void AssignData()
        {
            //assign data to variables
            FirstName = txtFirstName.Text;
            LastName = txtLastName.Text;
            Password = txtPassword.Text;
            InWorldLocation = txtLocation.Text;
            //BotCoordinates = txtCoordinates.Text;

            //Get Start coordinates, parse string into array, set x,y,z variables
            string coordinates = txtCoordinates.Text;
            string[] TestCoordinates = (coordinates.Split(new char[] { ',' }));
            x = TestCoordinates[0];
            y = TestCoordinates[1];
            z = TestCoordinates[2];
        }
        private void CreateBot()
        {
            string createDirectory;

            //create New Bot folder
            createDirectory = System.Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName;
            Directory.CreateDirectory(createDirectory);

            // create New Bot "Events" folder
            Directory.CreateDirectory(createDirectory + "\\Events");

            // create New Bot "AIML" folder
            Directory.CreateDirectory(createDirectory + "\\AIML");

            writeBotConfig();
            writeEventsXml();
            writeQuestionsXml();
            copyBotSettingsTemplateToNewBot();

            MessageBox.Show("Data Sucessfully Saved", "Data Saved", MessageBoxButtons.OK);

            //clear data fields
            ClearFields();
        }
        private void ClearFields()
        {
            //clear all text boxes
            txtFirstName.ResetText();
            txtLastName.ResetText();
            txtPassword.ResetText();
            txtCoordinates.ResetText();

            //clear all variables
            FirstName = "";
            LastName = "";
            Password = "";
            InWorldLocation = "";
            AIMLpath = "";
            SettingsPath = "";

            //change any yellow error fields back to white
            txtFirstName.BackColor = Color.White;
            txtLastName.BackColor = Color.White;
            txtPassword.BackColor = Color.White;
            txtLocation.BackColor = Color.White;
            txtCoordinates.BackColor = Color.White;
        }
        private void writeBotConfig()
        {
            string directory;
            string filePath;
            AIMLpath = "\\Bots\\" + FirstName + " " + LastName + "\\AIML";
            SettingsPath = "\\Bots\\" + FirstName + " " + LastName + "\\" + FirstName + " " + LastName + ".xml";

            //set directory
            directory = System.Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName;

            if (Directory.Exists(directory))
            {
                XmlTextWriter xwriter = null;
                try
                {
                    filePath = directory + "\\BotConfig.xml";

                    xwriter = new XmlTextWriter(new StreamWriter(filePath));

                    // Set formatting options
                    xwriter.Formatting = Formatting.Indented;

                    // Write Xml Declaration
                    xwriter.WriteStartDocument();

                    // Write Elements to file
                    xwriter.WriteStartElement("BotData");
                    xwriter.WriteElementString("FirstName", FirstName);
                    xwriter.WriteElementString("LastName", LastName);
                    xwriter.WriteElementString("Password", Password);
                    xwriter.WriteElementString("Location", InWorldLocation);
                    xwriter.WriteElementString("X", x);
                    xwriter.WriteElementString("Y", y);
                    xwriter.WriteElementString("Z", z);
                    xwriter.WriteElementString("AIMLPath", AIMLpath);
                    xwriter.WriteElementString("BotSettingsPath", SettingsPath);
                    xwriter.WriteEndElement();

                    // Close the file
                    xwriter.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Problem writing to Xml document: \n\n" + e.ToString());
                }
                finally
                {
                    xwriter.Close();
                }
            }
        }
        private void writeEventsXml()
        {
            string directory;
            string filePath;

            directory = Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName + "\\Events";

            if (Directory.Exists(directory))
            {
                XmlTextWriter xwriter = null;
                try
                {
                    filePath = directory + "\\events.xml";

                    xwriter = new XmlTextWriter(new StreamWriter(filePath));
                    xwriter.Formatting = Formatting.Indented;

                    xwriter.WriteStartDocument();
                    xwriter.WriteStartElement("events");
                    xwriter.WriteString("");
                    xwriter.WriteEndElement();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Error writing to the events.xml file: \n\n" + e.ToString());
                }
                finally
                {
                    xwriter.Close();
                }
            }
        }
        private void writeQuestionsXml()
        {
            string directory;
            string filePath;

            directory = Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName + "\\Events";

            if (Directory.Exists(directory))
            {
                XmlTextWriter xwriter = null;

                try
                {
                    filePath = directory + "\\questions.xml";

                    xwriter = new XmlTextWriter(new StreamWriter(filePath));
                    xwriter.Formatting = Formatting.Indented;

                    xwriter.WriteStartDocument();
                    xwriter.WriteStartElement("movementQuestions");
                    xwriter.WriteStartElement("Question");
                    xwriter.WriteAttributeString("eventID", "0");
                    xwriter.WriteString("This is a dummy question/event and should never be called, but is needed to load bots with no movement questions");
                    xwriter.WriteEndElement();
                    xwriter.WriteEndElement();

                    xwriter.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Error writing to the questions.xml file: \n\n" + e.ToString());
                }
                finally
                {
                    xwriter.Close();
                }
            }
        }
        private void copyBotSettingsTemplateToNewBot()
        {
            try
            {
                string templatePath;
                string filePath;

                templatePath = Environment.CurrentDirectory + "\\Bots\\Bot Settings Template.xml";
                filePath = Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName + "\\" + FirstName + " " + LastName + ".xml";
                File.Copy(templatePath, filePath);
                editNewBotPredicates(filePath);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error copying Bot Settings: " + e.ToString());
            }
        }
        private void editNewBotPredicates(string filePath)
        {
            XmlDocument doc = null;

            try
            {
                // Load Xml document
                doc = new XmlDocument();
                doc.Load(filePath);

                // Create attribute and set value (value is the first name of the new bot)
                XmlAttribute nameAttribute = doc.CreateAttribute("value");
                nameAttribute.Value = FirstName;

                // Get a list containing every node that starts with "item"
                XmlNodeList items = doc.GetElementsByTagName("item");

                // Contains a list of arrtibutes for each item node in list
                XmlAttributeCollection tempCollection;

                for (int i = 0; i < items.Count; i++)
                {
                    // Point to the current attributes
                    tempCollection = items[i].Attributes;

                    // Loop through each attribute
                    for (int j = 0; j < tempCollection.Count; j++)
                    {
                        // Find the attribute with stated value
                        if (tempCollection[j].Value == "Insert Name")
                        {
                            // Remove the generic attribute
                            tempCollection.Remove(tempCollection[j]);

                            // Insert custom attribute (the name of the bot)
                            tempCollection.Append(nameAttribute);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error editing new bot settings file: \n\n" + e.ToString());
            }
            finally
            {
                doc.Save(filePath);
            }
        }

        #endregion

        #region Event Methods (Callbacks)

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Verifry user input
            VerifyFields();
            if (Error == true)
            {
                Error = false;
                return;
            }

            //if all fields check out
            AssignData();
            CreateBot();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        #endregion
    }
}