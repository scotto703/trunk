//**************************************************************
// Class: BotEventReader
//
// Author: Joel McClain
//
// Date: 10-20-09
//
// Description: Processes events in the events.xml file for each
//              bot.  Calls BotAction for all actions, BotMove for 
//              all movements and BotChat for all conversation.
//***************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using OpenMetaverse;

namespace BotGUI
{
    class BotEventReader
    {
        #region Attributes
        GridClient client; 
        BotMove botMovement;
        BotAction action;
        string eventFilePath;
        string questionsPath;
        List<questionsAndEvents> movementQuestions = new List<questionsAndEvents>();        
        private struct questionsAndEvents
        {
            public string question;
            public int eventID;
        }           
        #endregion

        #region Constructor
        public BotEventReader(GridClient client, string name)
        {
            this.client = client; 
            botMovement = new BotMove(client);
            action = new BotAction(client);
            eventFilePath = Environment.CurrentDirectory + "\\Bots\\" + name + "\\Events\\events.xml";
            questionsPath = Environment.CurrentDirectory + "\\Bots\\" + name + "\\Events\\questions.xml";                       
            loadQuestions();
        }
        #endregion       

        #region Methods
        /// <summary>
        /// Loads all current movement questions into movementQuestions list
        /// </summary>
        void loadQuestions()
        {
            // Open the questions.xml file
            XmlTextReader QReader = new XmlTextReader(new StreamReader(questionsPath));

            try
            {
                bool finished = false;

                // process the data in the file
                while (QReader.Read() && !finished)
                {
                    switch (QReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (QReader.Name == "Question")
                            {
                                // create a temporary structure to load the data into
                                questionsAndEvents tempStruct = new questionsAndEvents();

                                // Read in the eventID attribute
                                while (QReader.MoveToNextAttribute())
                                {
                                    // read the name and discard it
                                    string name = QReader.Name;

                                    // read the event number for this question (needed to launch event when question is asked)
                                    tempStruct.eventID = Int32.Parse(QReader.Value);
                                }

                                // read the question and store it in the struct
                                QReader.Read();
                                tempStruct.question = QReader.Value;

                                // add the structure to the list of available questions
                                movementQuestions.Add(tempStruct);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            // end of file
                            if (QReader.Name == "movementQuestions")
                                finished = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error reading questions.xml file: \n\n" + e.ToString());
            }
            finally
            {
                QReader.Close();
            }
        }

        /// <summary>
        /// Checks to see if message equals any movement questions.  If so, 
        /// the program calls the desired event
        /// </summary>
        /// <param name="message">string to compare to questions</param>
        public bool findQuestionAndLoadEvent(string message)
        {            
            bool movementExecuted = false;

            foreach (questionsAndEvents fireEvent in movementQuestions)
            {
                if (fireEvent.question == message)
                {
                    findEventInXmlFile(fireEvent.eventID);
                    movementExecuted = true;
                }
            }
            return movementExecuted;
        }              

        /// <summary>
        /// This method locates the event in the events file that is being called 
        /// and then calls loadObject() to process the event. If the event is not
        /// found, the file is closed.
        /// </summary>
        /// <param name="eventNum">Number that represents which event to processs</param>
        public void findEventInXmlFile(int eventNum) 
        {
            XmlTextReader reader = new XmlTextReader(eventFilePath);
           
            bool eventFound = false;

            try
            {
                while (reader.Read() && !eventFound)
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "event")
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    try
                                    {
                                        string attribName = reader.Name;
                                        string attribValue = reader.Value;

                                        if (eventNum == Int32.Parse(attribValue))
                                        {
                                            eventFound = true;
                                            loadObject(reader);
                                        }
                                    }
                                    catch (FormatException fe)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Error reading events file: \n\n" + fe.ToString());
                                    }
                                }
                            }
                            break;

                        case XmlNodeType.EndElement:
                            // Event not found.  Close the file.
                            if (reader.Name == "events")
                                eventFound = true;                            
                            break;
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// This method processes which objects to invoke (BotMove, BotAcion, BotChat) 
        /// and calls the correct helper method.  When done processing the objects and
        /// the end of the event is reached, the file is closed.
        /// </summary>
        /// <param name="reader">Conatins the position in the XML file that needs processed</param>
        private void loadObject(XmlTextReader reader)
        {
            bool eventLoaded = false;

            while (reader.Read() && !eventLoaded)
            {                
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "movement")
                            loadBotMoveMethod(reader);
                        else if (reader.Name == "action")
                            loadBotActionMethod(reader);
                        else if (reader.Name == "chat")
                            loadChat(reader);                                          
                        break;

                    case XmlNodeType.EndElement:
                        // End of event. Close the file.
                        if (reader.Name == "event")                       
                            eventLoaded = true;                        
                        break;
                }
            }
        }

        /// <summary>
        /// This method is processed when a BotMove object is needed.  It determines 
        /// which method of BotMove needs to be called and then reads in the appropriate
        /// variables and calls the method.
        /// </summary>
        /// <param name="reader">Contains the position in the XML file that needs processed</param>
        private void loadBotMoveMethod(XmlTextReader reader)
        {              
            bool endOfMethod = false;

            while (reader.Read() && !endOfMethod)
            {
                switch (reader.NodeType)
                {
                    // Here we call the correct BotMove method.  Any method added to BotMove will
                    // need to be included here for processing.

                    case XmlNodeType.Element:
                        if (reader.Name == "moveTo")
                        {
                            // BotMove.moveTo() has been called and now we load the Vector needed
                            // for the method call                                             
                            botMovement.moveTo(readVectorCoord(reader));
                        }
                        else if (reader.Name == "Teleport")
                        {
                            bool methodLoaded = false;

                            while (reader.Read() && !methodLoaded)
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        if (reader.Name == "Region")
                                        {
                                            reader.Read();
                                            botMovement.setRegionName(reader.Value);                                           
                                        }
                                        if (reader.Name == "Vector")
                                        {
                                            botMovement.teleportTo(readVectorCoord(reader));                                            
                                        }
                                        break;
                                    case XmlNodeType.EndElement:
                                        if (reader.Name == "Teleport")
                                            methodLoaded = true;
                                        break;
                                }
                            }
                        }
                        break;
                    case XmlNodeType.EndElement:
                        // Here we look for the closing element for the method name.  Once reached 
                        // we end the loop and give control back to calling method (loadObject)
                        if (reader.Name == "movement")
                            endOfMethod = true;
                        break;
                }
            }         
        }

        /// <summary>
        /// All Vector3 objects have and x, y, and z coordinate.  Here we will
        /// read 3 nodes in Xml that will contian the coordinates and then create 
        /// a Vector3 object and return it.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Vector3 readVectorCoord(XmlTextReader reader)
        {             
            float[] variables = new float[3];            
            for (int i = 0; i < 3; i++)
            {
                reader.Read(); // read the whitespace
                reader.Read(); // read next element name                
                reader.Read(); // read next text coordinate value
                variables[i] = Single.Parse(reader.Value); // copy coordinate value into array
                reader.Read();  // read the end element                
            }
            return new Vector3(variables[0], variables[1], variables[2]);            
        }

        /// <summary>
        /// This method is processed when a BotAction object is needed. This will read what 
        /// variables need to be set for the action that needs to happen and then reads the 
        /// variables needed to call the createAction method (actionType and Time)
        /// </summary>
        /// <param name="reader">Contains the position in the XML file that needs processed</param>
        private void loadBotActionMethod(XmlTextReader reader)
        {            
            // Loads all variables needed and then calls BotAction.creatAction() 
            
            bool endOfMethod = false;
            int actionType = 0; // this will be set to the correct action number
            int time = 0;  // if needed, this will be set to alloted time for action

            while (reader.Read() && !endOfMethod)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "createAction")
                        {
                            bool methodLoaded = false;

                            // read and set the variables for this action
                            // and read the parameters for the createAction 
                            // method (actionType and time)
                            while (reader.Read() && !methodLoaded)
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        if (reader.Name == "UUID")
                                        {
                                            reader.Read();
                                            action.setUUID(new UUID(reader.Value));
                                        }
                                        else if (reader.Name == "InvItem")
                                        {
                                            reader.Read();
                                            UUID tmpUUID = new UUID(reader.Value);
                                            action.setInvItem((new InventoryItem(tmpUUID)));
                                        }
                                        else if (reader.Name == "AttachPoint")
                                        {
                                            reader.Read();
                                            AttachmentPoint point = (AttachmentPoint) Enum.Parse(typeof(AttachmentPoint), reader.Value,true);
                                            action.setAtchPoint(point);
                                        }
                                        else if (reader.Name == "Vector")
                                        {
                                            action.setVector3(readVectorCoord(reader));
                                        }
                                        else if (reader.Name == "actionType")
                                        {
                                            reader.Read();
                                            actionType = Int32.Parse(reader.Value);
                                        }
                                        else if (reader.Name == "Timer")
                                        {
                                            reader.Read();
                                            time = Int32.Parse(reader.Value);
                                        }
                                        else if (reader.Name == "SleepTime")
                                        {
                                            reader.Read();
                                            string randomTime = reader.Value;
                                            if (randomTime == "random")
                                                action.setRandom(true);
                                            else 
                                                action.setTime(Int32.Parse(randomTime));
                                        }
                                        break;
                                    case XmlNodeType.EndElement:
                                        // all variables should've loaded for action.                                        
                                        if (reader.Name == "createAction")
                                        {
                                            // call createAction to perform the action
                                            action.createAction(actionType, time);
                                            methodLoaded = true;
                                        }                                        
                                        break;
                                }
                            }

                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "action")
                            endOfMethod = true;
                        break;
                }
            }
        }

        /// <summary>
        /// This method will allow a bot to chat in-world
        /// </summary>
        /// <param name="message">sentence that gets said in-world</param>
        private void loadChat(XmlTextReader reader)
        {
            bool methodLoaded = false;

            while (reader.Read() && !methodLoaded)
            {
                try
                {
                    string message = reader.Value;  // throws format exception if there is no data to read 
                    client.Self.Chat(message, 0, ChatType.Normal);
                    methodLoaded = true;
                    reader.Read();  // read the closing chat tag </chat>
                }
                catch (FormatException fe)
                {
                    System.Windows.Forms.MessageBox.Show("Error: Could not read chat output\n\n" + fe.ToString());
                }
            }
        }       
        #endregion
    }     
}
