//**************************************************************
// Class: BotLoad
// 
// Author: Joel McClain
//
// Date: 12-2-09
//
// Description: It all starts from this class.  It logs in bots,
//              loads the AIML, positions bots, loads callback 
//              methods, and processes all movement code.               
//
//**************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenMetaverse.Utilities;
using OpenMetaverse.Packets;
using OpenMetaverse.GUI;
using OpenMetaverse;
using System.Xml;
using System.IO;
using System.Reflection;
using AIMLbot;
using AIMLbot.Utils;
using System.Threading;
using System.Globalization;
using System.Timers;

namespace BotGUI
{
    class BotLoad : GridClient
    {
        #region Attributes

        string botName;
        public string Name
        {
            get
            {
                return botName;
            }
            set
            {
                botName = value;
            }
        }                       
        System.Timers.Timer radiusTimer = new System.Timers.Timer();
        System.Timers.Timer ListTimer = new System.Timers.Timer();
        BotLoadAIML AimlChatterBot = null;                
        BotEventReader eventReader = null;
        List<User> people = new List<User>();
        XmlDocument BotConfigXML = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">name of bot to load</param>
        public BotLoad(string name)
        {
            botName = name;            
            AimlChatterBot = new BotLoadAIML(Name);
            eventReader = new BotEventReader(this, Name);
            BotConfigXML = new XmlDocument();
            BotConfigXML.Load(Environment.CurrentDirectory + "\\Bots\\" + name + "\\BotConfig.xml");
        }

        #endregion Constructor

        #region Bot Configuration Methods

        private string getBotFirstName()
        {             
            XmlNode node = BotConfigXML.SelectSingleNode("//FirstName");            
            return node.InnerText;
        }
        private string getBotLastName()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//LastName");
            return node.InnerText;
        }
        private string getBotPassword()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//Password");
            return node.InnerText;
        }
        private string getBotStartLocation()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//Location");
            return node.InnerText;
        }
        private int getBotStartXCoordinate()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//X");            
            return Int32.Parse(node.InnerText);
        }
        private int getBotStartYCoordinate()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//Y");
            return Int32.Parse(node.InnerText);
        }
        private int getBotStartZCoordinate()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//Z");
            return Int32.Parse(node.InnerText);
        }
        private void getBotAIMLPath()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//AIMLPath");
            AimlChatterBot.AimlPath = Environment.CurrentDirectory + node.InnerText;
        }
        private void getBotSettingsPath()
        {
            XmlNode node = BotConfigXML.SelectSingleNode("//BotSettingsPath");
            AimlChatterBot.SettingsPath = Environment.CurrentDirectory + node.InnerText;
        }

        #endregion

        #region Methods
        public GridClient getClient()
        {
            return this;
        }        
        public void LogBotOut()
        {                        
            this.Network.Logout();            
        }
        public void InitBot()
        {                        
            // Get bots startup information            
            getBotAIMLPath();
            getBotSettingsPath();
                     
            // create login parameters struct
            LoginParams clientLogin;

            // ****************************************************************************
            // These are my test statements - 1
            // ****************************************************************************
            
            // this.Settings.LOGIN_SERVER = "http://127.0.0.1:9000/";

            // ****************************************************************************
            // End test statements - 1
            // ****************************************************************************
            
            // populate login struct properties

            clientLogin = this.Network.DefaultLoginParams(getBotFirstName(), getBotLastName(), getBotPassword(), "Baker Island Bots", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            
            // ****************************************************************************
            // Temporarily Commented Out so I don't forget what I did
            // ****************************************************************************

            
            this.Settings.USE_LLSD_LOGIN = true;

            // ****************************************************************************
            // End Temporarily Commented Out
            // ****************************************************************************

            this.Settings.USE_ASSET_CACHE = true;

            // ****************************************************************************
            // These are my test statements - 2
            // ****************************************************************************

            // NOW EMPTY
           
            // ****************************************************************************
            // End Test Statements - 2
            // ****************************************************************************

            // register events for this bot            
            radiusTimer.Elapsed += new ElapsedEventHandler(OnTimedEventRadius);
            ListTimer.Elapsed += new ElapsedEventHandler(OnTimedEventList);             
            this.Network.Disconnected += new EventHandler<DisconnectedEventArgs>(Network_Disconnected);
            this.Self.ChatFromSimulator += new EventHandler<ChatEventArgs>(Self_ChatFromSimulator);
            this.Self.ChatFromSimulator += new EventHandler<ChatEventArgs>(Log_ChatFromSimulator);
            this.Self.IM += new EventHandler<InstantMessageEventArgs>(Self_IM);
            this.Network.LoginProgress += new EventHandler<LoginProgressEventArgs>(Network_LoginProgress);
            this.Network.EventQueueRunning += new EventHandler<EventQueueRunningEventArgs>(Network_EventQueueRunning);
                                   
            // Sets the timers
            radiusTimer.Interval = 7200000;
            radiusTimer.Enabled = true;
            ListTimer.Interval = 7200000;
            ListTimer.Enabled = true;
            
            // Load AIMLBot settings for this bot
            AimlChatterBot.myBot.loadSettings(AimlChatterBot.SettingsPath);
            AimlChatterBot.myBot.isAcceptingUserInput = false;
            AimlChatterBot.myBot.isAcceptingUserInput = true;

            // log bot in
            this.Network.BeginLogin(clientLogin);                       
        }                                    
        public void botLoadDefaultAIML()
        {
            AimlChatterBot.Loader.loadAIML(Environment.CurrentDirectory + "\\defaultAIML");
        }
        public void botLoadSpecficAIML()
        {
            AimlChatterBot.Loader.loadAIML(AimlChatterBot.AimlPath);
        }
        public void botLoadAIML(string path)
        {
            AimlChatterBot.Loader.loadAIML(path);
        }      
        public void PositionBot()
        {
            // Every bot has a postion event, in their events.xml file, the event number is always 0
            eventReader.findEventInXmlFile(0);            
        }
        #endregion Methods

        #region Event Methods (CallBacks) 
        void Network_EventQueueRunning(object sender, EventQueueRunningEventArgs e)
        {
            if (this.Self.Name == "Alisandra Cascarino" || this.Self.Name == "Nimon Herbit")
            {
                bool runEventForever = true;

                while (runEventForever == true)
                {
                    eventReader.findEventInXmlFile(1);
                }
            }
        }
        void Network_LoginProgress(object sender, LoginProgressEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                // Unregister for this event, since we are now logged in
                this.Network.LoginProgress -= Network_LoginProgress;

                this.Self.Chat("Hi everyone!", 0, ChatType.Normal);
                PositionBot();
            }
        }
        void Self_ChatFromSimulator(object sender, ChatEventArgs e)
        {
            int radius = 7; //set the radius (in meters) avatars must be within to chat with bots
            Vector3 botPos = this.Self.SimPosition; //current location of bot
            Vector3 avatarPos = e.Position; //current location of avatar
            Boolean movementExecuted = false;

            //Don't allow bots to respond to themselves, each other, or empty messages
            if ((e.SourceID != this.Self.AgentID) &&
                (e.FromName != "Alisandra Cascarino") &&
                (e.FromName != "Britney Luminos") &&
                (e.FromName != "Chesterfield Wrigglesworth") &&
                (e.FromName != "Elminstyr Exonar") &&
                (e.FromName != "Franklin Fiertze") &&
                (e.FromName != "Oriana Inglewood") &&
                (e.FromName != "Tracy Helstein") &&
                (e.FromName != "Counselor Silversmith") &&
                (e.FromName != "William Ormidale") &&
                (e.FromName != "Nimon Herbit") &&
                (e.Message != ""))
            {
                //Don't allow bots to respond to avatars on different floors or beyond (radius) meters away
                //Must use SimPosition (RelativePosition returns position from object bot is sitting on)
                if (((this.Self.Name == "Alisandra Cascarino") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Britney Luminos") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Chesterfield Wrigglesworth") && (e.Position.Z >= this.Self.SimPosition.Z - 1) ||
                    (this.Self.Name == "Elminstyr Exonar") && (e.Position.Z >= this.Self.SimPosition.Z - 1) ||
                    (this.Self.Name == "Franklin Fiertze") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Oriana Inglewood") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "William Ormidale") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Tracy Helstein") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Counselor Silversmith") && (e.Position.Z <= this.Self.SimPosition.Z + 1) ||
                    (this.Self.Name == "Nimon Herbit") && (e.Position.Z <= this.Self.SimPosition.Z + 1)) &&
                    ((avatarPos != Vector3.Zero) && (Vector3.Distance(avatarPos, botPos) < radius)))
                {
                    //turn towards speaking avatar
                    this.Self.Movement.TurnToward(e.Position);

                    bool found = false;
                    int peopleIndex = 0;

                    //search people list for current user
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].UserID == e.FromName)
                            found = true;
                    }

                    //if not found add current user to list
                    if (found == false)
                    {
                        User chatUser = new User(e.FromName, AimlChatterBot.myBot);
                        people.Add(chatUser);

                    }

                    //get index of current user from list
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].UserID == e.FromName)
                            peopleIndex = i;
                    }

                    //stores bot response from preceding occurance of Self_OnChat
                    //this is sent to BotMovement to test if movement should occur
                    if (AimlChatterBot.chatResult != null)
                    {
                        AimlChatterBot.chatQuestion = AimlChatterBot.chatResult.RawOutput;
                    }

                    //send user message to AIMLbot.Request for encapsulation
                    AimlChatterBot.chatRequest = new Request(e.Message, people[peopleIndex], AimlChatterBot.myBot);

                    //send encapsulated user message for AIML response
                    AimlChatterBot.chatResult = AimlChatterBot.myBot.Chat(AimlChatterBot.chatRequest);

                    //If user answers 'yes' (request) to a bot question (result), test to see if 
                    //it was a request to change location
                    if (AimlChatterBot.chatRequest.rawInput.ToUpper() == "YES")
                    {
                        movementExecuted = eventReader.findQuestionAndLoadEvent(AimlChatterBot.chatQuestion);
                    }

                    //Only display bot reply if previous user answer did not create botMovement
                    if (movementExecuted == false)
                    {
                        this.Self.AnimationStart(Animations.TYPE, false);
                        Thread.Sleep(3000);
                        this.Self.AnimationStop(Animations.TYPE, true);

                        //use this to not include avatar name in each reply
                        this.Self.Chat(AimlChatterBot.chatResult.Output, 0, ChatType.Normal);
                    }                    
                }
            }
        }        
        void Log_ChatFromSimulator(object sender, ChatEventArgs e)
        {
            string messageToWrite = e.Message;
            string bot = this.Self.Name;
            string avatarName = e.FromName;
            string dirName = "ChatLog";
            DateTime worldTime = DateTime.Now;
            string filePath = Environment.CurrentDirectory + "\\Bots\\" + botName + "\\" + dirName;

            DirectoryInfo dInfo = new DirectoryInfo(filePath);

            if (avatarName == this.Self.Name ||
                ((avatarName != "Alisandra Cascarino") &&
                 (avatarName != "Britney Luminos") &&
                 (avatarName != "Chesterfield Wrigglesworth") &&
                 (avatarName != "Elminstyr Exonar") &&
                 (avatarName != "Franklin Fiertze") &&
                 (avatarName != "Oriana Inglewood") &&
                 (avatarName != "Tracy Helstein") &&
                 (avatarName != "Counselor Silversmith") &&
                 (avatarName != "William Ormidale") &&
                 (avatarName != "Nimon Herbit")))
            {
                //Checks to see if the chat folder exists. If it doesn't, it is created.
                if (!dInfo.Exists)
                {
                    dInfo.Create();
                }

                if (messageToWrite != null && messageToWrite.Length != 0) //check for empty messages
                {
                    try
                    {
                        StreamWriter chat = new StreamWriter(filePath + "\\" + bot + ".txt", true);
                        chat.WriteLine(worldTime + ": " + avatarName + ": " + messageToWrite);
                        chat.Close();
                    }
                    catch (IOException)
                    {
                        // Do nothing as of yet ... should this be logged?                      
                    }                    
                }
            }
        }                        
        void Self_IM(object sender, InstantMessageEventArgs e)
        {
            Boolean movementExecuted = false;

            if (e.IM.Dialog == InstantMessageDialog.MessageFromAgent)
            {
                //turn towards speaking avatar
                this.Self.Movement.TurnToward(e.IM.Position);

                bool found = false;
                int peopleIndex = 0;

                //search people list for current user
                for (int i = 0; i < people.Count; i++)
                {
                    if (people[i].UserID == e.IM.FromAgentName.ToString())
                        found = true;
                }

                //if not found add current user to list
                if (found == false)
                {
                    User newIMuser = new User(e.IM.FromAgentName.ToString(), AimlChatterBot.myBot);
                    people.Add(newIMuser);
                }

                //get index of current user from list
                for (int i = 0; i < people.Count; i++)
                {
                    if (people[i].UserID == e.IM.FromAgentName.ToString())
                        peopleIndex = i;
                }

                //stores bot response from preceding occurance of Self_OnChat
                //this is sent to BotMovement to test if movement should occur
                if (AimlChatterBot.imResult != null)
                {
                    AimlChatterBot.imQuestion = AimlChatterBot.imResult.RawOutput;
                }

                //send user message to AIMLbot.Request for encapsulation
                AimlChatterBot.imRequest = new Request(e.IM.Message, people[peopleIndex], AimlChatterBot.myBot);

                //send encapsulated user message for AIML response
                AimlChatterBot.imResult = AimlChatterBot.myBot.Chat(AimlChatterBot.imRequest);

                //If user answers 'yes' (request) to a bot question (result), test to see if 
                //it was a request to change location
                if (AimlChatterBot.imRequest.rawInput.ToUpper() == "YES")
                {
                    movementExecuted = eventReader.findQuestionAndLoadEvent(AimlChatterBot.chatQuestion);
                }

                //Only display bot reply if previous user answer did not create botMovement
                if (movementExecuted == false)
                {
                    this.Self.AnimationStart(Animations.TYPE, false);
                    Thread.Sleep(3000);
                    this.Self.AnimationStop(Animations.TYPE, true);
                    this.Self.InstantMessage(e.IM.FromAgentID, AimlChatterBot.imResult.Output, e.IM.IMSessionID);
                }
            }            
        }        
        void Network_Disconnected(object sender, DisconnectedEventArgs e)
        {
            if (e.Reason == NetworkManager.DisconnectType.NetworkTimeout)
            {
                System.Windows.Forms.MessageBox.Show(this.Self.Name + " was disconnected." + "\n\n" + e.Reason.ToString());                                
            }
        }       
        void OnTimedEventList(object source, ElapsedEventArgs e)
        {
            people.Clear();
        }
        void OnTimedEventRadius(object source, ElapsedEventArgs e)
        {
            int radius = 10; //set the radius (in meters) to scan
            Vector3 mypos = this.Self.SimPosition;

            //populate list with object in the radius
            List<Avatar> avatars = this.Network.CurrentSim.ObjectsAvatars.FindAll(delegate(Avatar avatar)
            {
                Vector3 pos = avatar.Position;
                Vector3 location = mypos;
                return ((avatar.ParentID == 0) && (pos != Vector3.Zero) && (Vector3.Distance(pos, location) < radius));
            }
               );
            bool found = false;
            for (int i = 0; i < avatars.Count; i++)
            {
                for (int j = 0; j < people.Count; j++)
                {
                    if (people[j].UserID == avatars[i].Name)
                    {
                        found = true;
                    }
                }

                if ((found == false) &&
                    (avatars[i].Name != this.Self.Name) &&
                    (avatars[i].Name != "Alisandra Cascarino") &&
                    (avatars[i].Name != "Britney Luminos") &&
                    (avatars[i].Name != "Chesterfield Wrigglesworth") &&
                    (avatars[i].Name != "Elminstyr Exonar") &&
                    (avatars[i].Name != "Franklin Fiertze") &&
                    (avatars[i].Name != "Counselor Silversmith") &&
                    (avatars[i].Name != "Oriana Inglewood") &&
                    (avatars[i].Name != "Tracy Hydefeld"))
                {
                    this.Self.AnimationStart(Animations.TYPE, false);
                    Thread.Sleep(3000);
                    this.Self.AnimationStop(Animations.TYPE, true);
                    this.Self.Chat("Hello would you like to chat " + avatars[i].Name, 0, ChatType.Normal);
                }
            }
        }
        #endregion Events
    }
}
