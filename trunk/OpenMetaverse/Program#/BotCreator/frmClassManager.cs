//**************************************************************
// Class: frmClassManager
// 
// Author: Alan Jesser
//  
// Edited by: Phillip Brossia, Joel McClain
//
// Description: This is the main form for the program.                
//
//**************************************************************

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using OpenMetaverse;
using OpenMetaverse.GUI;

namespace BotGUI
{
    public partial class frmClassMngr : Form
    {
        #region Attributes
        public static frmClassMngr MainWindow = new frmClassMngr();                
        private int counter = 0; //counter for initial tab creation
        private List<BotLoad> runningBots = new List<BotLoad>();
        #endregion

        #region Constructor
        public frmClassMngr()
        {
            InitializeComponent();  // starts GUI
            GetBotList();  // populates list to load from
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
                BotName = BotList[n].Name;
                listAvailBot.Items.Add(BotName);                 
            }             
        }
        private void loadBot()
        {
            try
            {
                //item must be selected to launch Bot
                if (listAvailBot.SelectedItem != null)
                {
                    // flag to check if bot is already loaded
                    bool botLoaded = findBotLB(listAvailBot.SelectedItem.ToString());

                    // If bot is not loaded then we load bot here
                    if (!botLoaded)
                    {
                        // create BotLoad object for selected bot
                        BotLoad selectedBot = new BotLoad(listAvailBot.SelectedItem.ToString());
                        GridClient client = selectedBot.getClient();

                        // create Tab for selected bot
                        newTab(client);

                        // Starts bot in virtual world
                        selectedBot.InitBot();

                        // Adds bot to list of active bots
                        botLB.Items.Add(listAvailBot.SelectedItem.ToString());  

                        // Adds bot to list of running bots
                        runningBots.Add(selectedBot);
                    }
                    else
                    {
                        MessageBox.Show("Bot is already loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Need to select a name from the list", "Unable to Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FileNotFoundException fnf)
            {
                MessageBox.Show("Bot configuration files not found." + "\n\n" + fnf.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Tab was created for this bot, but it did not load. So we remove the tab.
                tabControl1.TabPages.RemoveByKey(listAvailBot.SelectedItem.ToString());
            }
            catch (DirectoryNotFoundException dnf)
            {
                MessageBox.Show("Bot directory not found." + "\n\n" + dnf.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Tab was created for this bot, but it did not load. So we remove the tab.
                tabControl1.TabPages.RemoveByKey(listAvailBot.SelectedItem.ToString());
            }            
        }
        private void newTab(GridClient client)
        {
            //instantiate a new tab with background color white
            TabPage newTab = new TabPage();
            newTab.BackColor = Color.White;

            // Creates status panel for current loading bot in
            // the bots tab
            StatusOutput botStatus = new StatusOutput();
            botStatus.Size = new System.Drawing.Size(537, 149);
            botStatus.Location = new Point(3, 6);
            botStatus.ReadOnly = true;
            botStatus.BackColor = Color.White;
            botStatus.Client = client;
            
            //instantiate a new aimlButton to match the startup button
            Button aimlButton = new Button();
            aimlButton.Size = new Size(100, 76);
            aimlButton.Location = new Point(546, 6);
            aimlButton.Text = "Change Path";
            aimlButton.UseVisualStyleBackColor = true;
            aimlButton.Click += new EventHandler(aimlButton_Click);

            //instantiate a new stopButton to match the startup button
            Button stopBotButton = new Button();
            stopBotButton.Size = new Size(100, 70);
            stopBotButton.Location = new Point(546, 88);
            stopBotButton.Text = "Stop Bot";
            stopBotButton.UseVisualStyleBackColor = true;
            stopBotButton.Click += new EventHandler(stopBotButton_Click);
           
            //determine if this is the first tab to create or not
            if (listAvailBot.SelectedIndex != -1 & counter == 0)
            {
                //if first bot, enable tabControl and remove the first
                //tab. The first tab does not output bot status.
                tabControl1.Enabled = true;
                tabControl1.TabPages.RemoveAt(0);
                
                // create 1st tab with bot status as output
                newTab.Controls.Add(botStatus);
                newTab.Controls.Add(aimlButton);
                newTab.Controls.Add(stopBotButton);
                newTab.Text = listAvailBot.SelectedItem.ToString();
                newTab.Name = listAvailBot.SelectedItem.ToString();
                tabControl1.Controls.Add(newTab);
                tabControl1.SelectTab(listAvailBot.SelectedItem.ToString());
            }
            else if (findBotLB(listAvailBot.SelectedItem.ToString()))
            {//if the bot is found in the botLB don't create a new tab
            }
            else if (findBotTab(listAvailBot.SelectedItem.ToString()))
            {//if the bot already has a tab don't create a new one
            }
            else
            {
                //create a new tab by adding each component 
                //set to focus on new tab
                newTab.Controls.Add(botStatus);
                newTab.Controls.Add(aimlButton);
                newTab.Controls.Add(stopBotButton);
                newTab.Text = listAvailBot.SelectedItem.ToString();
                newTab.Name = listAvailBot.SelectedItem.ToString();
                tabControl1.Controls.Add(newTab);
                tabControl1.SelectTab(listAvailBot.SelectedItem.ToString());
            }
            counter++;//add to the counter for first time bot loaded        
        }
        void aimlButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();

            // Check to see if a directory was selected
            if (f.SelectedPath != "")
            {
                try
                {
                    // Figure out which bot we want to change the AIML for
                    string temp = tabControl1.SelectedTab.ToString();
                    string botToChangeAIML = temp.Substring(10, (temp.Length - 11));

                    // Enumerate through list of running bots
                    for (int i = 0; i < runningBots.Count; i++)
                    {
                        // Find the bot selected
                        if (runningBots[i].Name == botToChangeAIML)
                        {
                            // Load new AIML files. Throws FileNotFountException if there 
                            // are no AIML files in the directory choosen
                            runningBots[i].botLoadAIML(f.SelectedPath);

                            // Gets the current time and formats it to format xx:xx
                            DateTime time = DateTime.Now;
                            string timeOut = time.ToString("t", DateTimeFormatInfo.InvariantInfo);                            

                            // Add text to output stating the time and which directory 
                            // the files were loaded from
                            StatusOutput status = (StatusOutput)(tabControl1.SelectedTab.Controls[0]);
                            status.AppendText("\n" + "[" + timeOut + "]" + " Loaded AIML files from: " + f.SelectedPath);                                                        
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("There are no AIML files in the directory you choose");
                }
            }                
        }
        void stopBotButton_Click(object sender, EventArgs e)
        {
            // Figure out which bot we want to log out
            string temp = tabControl1.SelectedTab.ToString();
            string botToLogOut = temp.Substring(10, (temp.Length - 11));

            // Logout the selected bot
            stopBot(botToLogOut);            
        }
        //function to find out if a bot is in the botLB already
        private Boolean findBotLB(string botName)
        {
            return botLB.Items.Contains(botName);
        }
        //function to find out if the bot has a current tab created
        private Boolean findBotTab(string botName)
        {
            if (tabControl1.TabPages.IndexOfKey(botName) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void botToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadBot(); // calls loadBot to actually load the bot
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox1 About = new frmAboutBox1();
            About.ShowDialog();
        }
        private void loadBotB_Click(object sender, EventArgs e)
        {
            loadBot(); // calls loadBot to actually load the bot
        }
        private void editBotB_Click(object sender, EventArgs e)
        {
            frmEditBot newEdit = new frmEditBot();
            newEdit.Show();  // brings up the editor form to set paths for bots
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // close classMngr
        }
        private void botManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEditBot newEdit = new frmEditBot();
            newEdit.Show();  // brings up the editor form to set paths for bots
        }
        private void botStopB_Click(object sender, EventArgs e) 
        {
            try
            {
                string botSelected = botLB.SelectedItem.ToString();
                stopBot(botSelected);                 
            }
            catch (NullReferenceException)
            {
                if (botLB.Items.Count == 0)
                {
                    MessageBox.Show("There are no bots logged in to stop", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select a bot from the active bots list to log them out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }                     
        }        
        private void newBotB_Click(object sender, EventArgs e)
        {
            frmCreate newBot = new frmCreate();
            newBot.Show();                        
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // check to see if there are bots logged in
            if (runningBots.Count > 0)
                // Log out all bots logged in
                foreach (BotLoad bot in runningBots)                
                    bot.LogBotOut();                  

            // Release resources and garbage collect them            
            this.Dispose(true);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            // Kills any open threads and closes the program
            Environment.Exit(0);
        }
        private void stopBot(string botToStop)
        {
            // Check to see if there are any bots logged in
            if (runningBots.Count > 0)
            {
                //Enumerate through list of bots logged in
                for (int i = 0; i < runningBots.Count; i++)
                {
                    // If bot selected is running, log the bot out
                    if (runningBots[i].Name == botToStop)
                    {
                        // Log bot out of virtual world
                        runningBots[i].LogBotOut();

                        // Remove bot from list of running bots
                        runningBots.Remove(runningBots[i]);

                        // Remove bots name from Active Bots list
                        botLB.Items.Remove(botToStop);

                        // Removes bot status panel from tabs
                        tabControl1.TabPages.RemoveByKey(botToStop);
                    }
                }            
            }
        }
        #endregion              
    }
}


