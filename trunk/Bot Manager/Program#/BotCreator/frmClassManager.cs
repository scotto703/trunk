//**************************************************************
// Class: frmClassManager
// 
// Author: Joel McClain, Alan Jesser
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
        /// <summary>
        /// counter for initial tab creation
        /// </summary>
        private int counter = 0;
        /// <summary>
        /// List of botloads, which means this is the list of running bots
        /// </summary>
        private List<BotLoad> runningBots = new List<BotLoad>();
        
        #endregion        

        #region Constructor

        public frmClassMngr()
        {
            InitializeComponent();  // starts GUI
            GetBotList();  // populates list to load from
        }

        #endregion       

        #region Button Methods

        private void aimlButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();

            // Check to see if a directory was selected
            if (f.SelectedPath != "")
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
                        // Load new AIML files
                        LoadAIMLTestFiles(runningBots[i], f.SelectedPath, f);
                    }
                }
            }
        }
        private void stopBotButton_Click(object sender, EventArgs e)
        {
            // Figure out which bot we want to log out
            string temp = tabControl1.SelectedTab.ToString();
            string botToLogOut = temp.Substring(10, (temp.Length - 11));

            // Logout the selected bot
            stopBot(botToLogOut);
        }
        private void loadBotB_Click(object sender, EventArgs e)
        {
            loadBot(); // calls loadBot to actually load the bot
        }
        private void editBotB_Click(object sender, EventArgs e)
        {
            EditBot_Display();            
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
                //No bots logged in
                if (botLB.Items.Count == 0)
                {
                    MessageBox.Show("There are no bots logged in to stop", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //No bot was selected
                else
                {
                    MessageBox.Show("Please select a bot from the active bots list to log them out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void newBotB_Click(object sender, EventArgs e)
        {
            frmCreate newBot = new frmCreate();
            newBot.ShowDialog();
            GetBotList();      
        }
        private void EditBot_Display()
        {
            // ***********************************************
            //                                               *
            // This method is called when a user clicks      * 
            // the edit bot button or when the user          *
            // chooses Bot Manager from the View drop down   *
            // menu.                                         *
            //                                               *
            //************************************************
             
            // Create instance of edit bot form
            frmEditBot newEdit = new frmEditBot();

            // displays the form.  ShowDialog() blocks the main form from being clickable until this form
            // is closed. Dispose() marks it for garbage collection
            newEdit.ShowDialog();
            newEdit.Dispose();

            // Reset the list of available bots
            GetBotList();           
        }

        #endregion

        #region Tool Strip Methods

        private void botToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadBot(); // calls loadBot to actually load the bot
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create the instnace of the form that will be displayed
            frmAboutBox1 About = new frmAboutBox1();

            // Shows the form, blocking the main form until it closes
            About.ShowDialog();

            // Marks it for garbage collection
            About.Dispose();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); 
            this.Dispose();  
        }
        private void EventViewerToolStripButton_Click(object sender, EventArgs e)
        {
            if (listAvailBot.Items.Count == 0)//quick fix to avoid error when there are no bots
                return;
            formEventViewer newEventViewer = new formEventViewer();
            newEventViewer.ShowDialog();
            newEventViewer.Dispose();
        }
        private void botManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditBot_Display();
        }
        
        #endregion

        #region Callback Methods

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // check to see if there are bots logged in
            if (runningBots.Count > 0)
                // Log out all bots that are logged in
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

        #endregion

        #region Methods
        /// <summary>
        /// Looks in bots folder and gets the name of each bot there
        /// </summary>
        private void GetBotList() 
        {
            listAvailBot.Items.Clear();

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
                    listAvailBot.Items.Add(BotName);
                }
            }             
        }
        /// <summary>
        /// Create and log the bot into Second Life
        /// </summary>
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

                        // create Tab for selected bot
                        newTab(selectedBot.getClient());

                        // Starts bot in virtual world and loads AIML files for the bot
                        selectedBot.InitBot();
                        LoadDefaultAIML(selectedBot);
                        LoadBotSpecificAIML(selectedBot);
                        
                        // Adds bot to list of active bots
                        botLB.Items.Add(listAvailBot.SelectedItem.ToString());  

                        // Adds bot to list of running bots
                        runningBots.Add(selectedBot);
                    }
                    else
                    {
                        //A bot instance of the requested bot already exists
                        MessageBox.Show("Bot is already loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    //No bot was selected from the list
                    MessageBox.Show("Need to select a name from the list", "Unable to Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FileNotFoundException)
            {
                //Missing files
                MessageBox.Show("Bot configuration files not found." + "\n\n" +
                                "Reasons why loading a bot would fail:" + "\n\n" +
                                "1. Bot is not registered with Second Life" + "\n" +
                                "2. Bot username and password are incorrect" + "\n" +
                                "3. No AIML files for bot (to add AIML files click edit bot button and then add AIML)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Tab was created for this bot, but it did not load. So we remove the tab.
                tabControl1.TabPages.RemoveByKey(listAvailBot.SelectedItem.ToString());
            }
            catch (DirectoryNotFoundException dnf)
            {
                //Missing folders
                MessageBox.Show("Bot directory not found." + "\n\n" + dnf.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Tab was created for this bot, but it did not load. So we remove the tab.
                tabControl1.TabPages.RemoveByKey(listAvailBot.SelectedItem.ToString());
            }            
        }
        /// <summary>
        /// Load AIML from the defaultAIML folder
        /// </summary>
        /// <param name="selectedBot">The botload instance belonging to the target bot</param>
        private void LoadDefaultAIML(BotLoad selectedBot)
        {
            try
            {
                selectedBot.botLoadDefaultAIML();
                AppendTextToBotTab("Loaded baseline AIML files");
            }
            catch (FileNotFoundException)
            {
                //Did not find any files to load
                AppendTextToBotTab("No baseline AIML files to load");
            }
            catch (Exception)
            {
                //catchall
                AppendTextToBotTab("Error loading baseline AIML files");
            }
        }
        /// <summary>
        /// Load AIML from the bot's AIML folder
        /// </summary>
        /// <param name="selectedBot">The botload instance belonging to the target bot</param>
        private void LoadBotSpecificAIML(BotLoad selectedBot)
        {
            try
            {
                selectedBot.botLoadSpecficAIML();
                AppendTextToBotTab("Loaded bot specific AIML files");
            }
            catch (FileNotFoundException)
            {
                //Did not find any AIML files belonging to the bot to load
                AppendTextToBotTab("No bot specific AIML files to load");
            }
            catch (Exception)
            {
                //catchall
                AppendTextToBotTab("Error loading bot specific files");
            }
            
        }
        /// <summary>
        /// Load AIML files found at the given path
        /// </summary>
        /// <param name="selectedBot">The botload instance belonging to the target bot</param>
        /// <param name="path">Path to the AIML files</param>
        /// <param name="folder">Folder containing the AIML files</param>
        private void LoadAIMLTestFiles(BotLoad selectedBot, string path, FolderBrowserDialog folder)
        {
            try
            {
                selectedBot.botLoadAIML(path);
                AppendTextToBotTab("Loaded AIML files from: " + folder.SelectedPath);
            }
            catch (FileNotFoundException)
            {
                //Did not find any AIML files at given path
                AppendTextToBotTab("No AIML files to load in selected folder"); 
            }
        }
        /// <summary>
        /// Adds a tab to the tab control show each bot that is logged in
        /// </summary>
        /// <param name="client">GridClient belonging to the bot</param>
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
        /// <summary>
        /// Add a new line to the status text box of a tab
        /// </summary>
        /// <param name="text">New line to be added</param>
        private void AppendTextToBotTab(string text)
        {
            // Gets current time to append to text
            DateTime time = DateTime.Now;
            string currentTime = time.ToString("t", DateTimeFormatInfo.InvariantInfo);

            // Gets the Status window for the bot 
            StatusOutput status = (StatusOutput)(tabControl1.SelectedTab.Controls[0]);
            status.ForeColor = System.Drawing.Color.Red;
            status.AppendText("\n" + "[" + currentTime + "] " + text);            
        }
        /// <summary>
        /// Returns true or false depending on if the bot is logged in
        /// Searches the active bot listbox
        /// </summary>
        /// <param name="botName">Name of the bot to search for</param>
        private Boolean findBotLB(string botName)
        {
            return botLB.Items.Contains(botName);
        }
        /// <summary>
        /// Returns true or false depending on if the bot already has a tab
        /// Searches the bot status tab control
        /// </summary>
        /// <param name="botName">Name of the bot to search for</param>
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
        /// <summary>
        /// Logs out a bot and removes it from the running bots list
        /// </summary>
        /// <param name="botToStop">Name of the bot to stop</param>
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
                        // tabControl1.TabPages.RemoveByKey(botToStop);
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    }
                }
            }
        }

        #endregion
    }
}


