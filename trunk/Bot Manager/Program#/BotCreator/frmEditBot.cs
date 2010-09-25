//******************************************
//*** Author: Allan Blackford
//*** Edited by: Joel McClain
//***
//***Last Modified: 12/7/09
//******************************************

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BotGUI
{
    public partial class frmEditBot : Form
    {
        #region Attributes
        private static string FilePath;       
        private static string FirstName = "";
        private static string LastName = "";
        private static string Password = "";
        private static string InWorldLocation = "";        
        static string x;
        static string y;
        static string z;
        private static string AIMLpath = "";
        private static string SettingsPath = "";
        private static Boolean Error = false;
        
        #endregion

        #region Constructor

        public frmEditBot()
        {
            InitializeComponent();            
            GetBotList();
        }
        #endregion

        #region Methods
        private void GetBotList()
        {
            string BotName;

            //get current directory and add bot list folder to path
            string BotPath = Directory.GetCurrentDirectory() + "\\Bots";

            //gets a list for files in the directory
            DirectoryInfo BotDirectory = new DirectoryInfo(BotPath);
            DirectoryInfo[] BotList = BotDirectory.GetDirectories();

            // Adds the names of each bot to the list of Available bots
            for (int n = 0; n < BotList.Length; n++)
            {
                if (!BotList[n].Attributes.ToString().Contains("Hidden"))
                {
                    BotName = BotList[n].Name;
                    cboBotList.Items.Add(BotName);
                }
            }
        }
        private void ReadBotFile()
        {
            string BotName = cboBotList.SelectedItem.ToString();
            FilePath = Environment.CurrentDirectory + "\\Bots\\" + BotName;
            XmlTextReader reader = new XmlTextReader(new StreamReader(FilePath + "\\BotConfig.xml"));

            try
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "FirstName")
                            {
                                reader.Read();
                                FirstName = reader.Value;
                            }
                            else if (reader.Name == "LastName")
                            {
                                reader.Read();
                                LastName = reader.Value;
                            }
                            else if (reader.Name == "Password")
                            {
                                reader.Read();
                                Password = reader.Value;
                            }
                            else if (reader.Name == "Location")
                            {
                                reader.Read();
                                InWorldLocation = reader.Value;
                            }
                            else if (reader.Name == "X")
                            {
                                reader.Read();
                                x = reader.Value;
                            }
                            else if (reader.Name == "Y")
                            {
                                reader.Read();
                                y = reader.Value;
                            }
                            else if (reader.Name == "Z")
                            {
                                reader.Read();
                                z = reader.Value;
                            }
                            else if (reader.Name == "AIMLPath")
                            {
                                reader.Read();
                                AIMLpath = Environment.CurrentDirectory + reader.Value;
                            }
                            else if (reader.Name == "BotSettingsPath")
                            {
                                reader.Read();
                                SettingsPath = Environment.CurrentDirectory + reader.Value;
                            }
                            break;                        
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Exception caught: \n\n" + e.ToString());
            }
            finally
            {
                reader.Close();
            }
        }
        private void DisplayData()
        {
            //fill text boxes
            txtFirstName.Text = FirstName;
            txtLastName.Text = LastName;
            txtPassword.Text = Password;
            txtLocation.Text = InWorldLocation;

            txtCoordinates.Text = x + "," + y + "," + z;

            //path is most likely too long for text box
            //display in tool tip box
            ToolTip.IsBalloon = true;
            ToolTip.SetToolTip(btnAIMLpath, AIMLpath);
            ToolTip.SetToolTip(btnSettingsPath, SettingsPath);

        }
        private void ClearData()
        {
            //clear all text boxes
            txtFirstName.ResetText();
            txtLastName.ResetText();
            txtPassword.ResetText();
            txtLocation.ResetText();
            txtCoordinates.ResetText();
            dlgAIMLPath.Reset();
            dlgSettingsPath.Reset();
        }
        private void VerifyFields()
        {
            // If a text box is blank, make its background color yellow and set Error to true
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
                MessageBox.Show("The yellow field(s) cannot be empty", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AIMLpath == "")
            {
                MessageBox.Show("You must specify an AIML folder for your bot.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Error = true;
                return;
            }

            if (SettingsPath == "")
            {
                MessageBox.Show("You must specify a Settings XML file for your bot.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Error = true;
                return;
            }

            //coordinate field can only contain integers
            //place coordinates into string array
            string coordinates = txtCoordinates.Text;
            string[] TestCoordinates = (coordinates.Split(new char[] { ',' }));

            //array must have 3 elements
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
                    MessageBox.Show("Coordinates can only be integers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //Get Start coordinates, parse string into array, set x,y,z variables 
            string coordinates = txtCoordinates.Text;
            string[] TestCoordinates = (coordinates.Split(new char[] { ',' }));
            x = TestCoordinates[0];
            y = TestCoordinates[1];
            z = TestCoordinates[2];
        }
        private void WriteFile()
        {
            string directory;
            string filePath;

            //set directory and file path
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
                    xwriter.WriteElementString("AIMLPath", "\\Bots\\" + FirstName + " " + LastName + "\\AIML");
                    xwriter.WriteElementString("BotSettingsPath", "\\Bots\\" + FirstName + " " + LastName + "\\" + FirstName + " " + LastName + ".xml");
                    xwriter.WriteEndElement();                    

                    MessageBox.Show("Data Sucessfully Saved", "Data Saved", MessageBoxButtons.OK);
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
            else
            {
                MessageBox.Show("The bot " + FirstName + " " + LastName + " does not exist.\n\n"
                                + "Can't edit a bot that does not exist.", "Bot Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Event Methods
        private void cboBotList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //retrieves bot data from select bot file
            ReadBotFile();            
            DisplayData();
        }
        private void btnAIMLpath_Click(object sender, EventArgs e)
        {
            dlgAIMLPath.ShowDialog();

            // Check to make sure cancel was not selected
            if (dlgAIMLPath.SelectedPath != "")
            {                
                DirectoryInfo di = new DirectoryInfo(dlgAIMLPath.SelectedPath);
                FileInfo[] fi = di.GetFiles("*.aiml");
                string desintationFolder = Environment.CurrentDirectory + "\\Bots\\" + FirstName + " " + LastName + "\\AIML";
                                
                foreach (FileInfo fileName in fi)
                {
                    try
                    {                       
                        File.Copy(dlgAIMLPath.SelectedPath + "\\" + fileName.Name, desintationFolder + "\\" + fileName.Name, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error copying file: \n\n" + ex.ToString());
                    }
                }                
            }
        }
        private void btnSettingsPath_Click(object sender, EventArgs e)
        {
            // ********************************************
            // This works but will make the bot unuseable *
            // if changed.                                *
            //*********************************************

            /*
            DialogResult userClickedOK = dlgSettingsPath.ShowDialog();             

            // Check to see if cancel was clicked
            if ( userClickedOK == DialogResult.OK)
            {
                SettingsPath = dlgSettingsPath.FileName.ToString();
                ToolTip.SetToolTip(btnSettingsPath, SettingsPath);
                ToolTip.ShowAlways = true;
            }
             * */
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Verifry user input
            VerifyFields();
            if (Error)
            {
                Error = false;
                return;
            }

            //if all fields check out load data variables
            AssignData();            

            //write data to file
            WriteFile();

            //remove selected text in combo box
            cboBotList.ResetText();

            //remove items in combo box
            cboBotList.Items.Clear();

            //clear text data fields
            ClearData();
            
            //refill combo box
            GetBotList();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete bot file
            DialogResult dlgResult;

            dlgResult = MessageBox.Show("WARNING, YOU ARE ABOUT TO DELETE A BOT!", "DELETE BOT", 
                                         MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dlgResult == DialogResult.OK)
            {
                //delete file
                Directory.Delete(FilePath, true); 
               
                // Reset list of selectable bots
                cboBotList.ResetText();
                cboBotList.Items.Clear();
                ClearData();
                GetBotList();                              
            }
         }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();            
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        #endregion
    }
}