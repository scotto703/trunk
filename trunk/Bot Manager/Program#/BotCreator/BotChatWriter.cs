//**************************************************************
// Class: BotChatWriter
// 
// Author: Francisco Scovino
//
// Date: 11/02/2010
//
// Description: This class writes all the chat methods into the
//              corresponding XML file             
//
//**************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace BotGUI
{
    class BotChatWriter
    {
        #region Attributes
 
        /// <summary>
        /// Xml doument the writer will write to
        /// </summary>
        XmlDocument XmlDoc;

        /// <summary>
        /// The number identifying the event
        /// </summary>
        int eventNumber;

        /// <summary>
        /// The name of the bot
        /// </summary>
        string botName;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="XmlFile">XmlDocument that is the document to write to</param>
        /// <param name="eventNum">Integer that is the number identifying the event</param>
        /// <param name="name">String that is the name of the bot</param>
        public BotChatWriter(XmlDocument XmlFile, int eventNum, string name)
        {
            this.XmlDoc = XmlFile;
            this.eventNumber = eventNum;
            this.botName = name;
        }

        #endregion

        /// <summary>
        /// Writes a chat event to the xml this writer is assigned to
        /// Uses event number given to writer through the constructor
        /// </summary>
        /// <param name="chatLine">String that is the message to be spoken</param>
        public void WriteChatToXml(string chatLine)
        {
            try
            {
                // Create Xml node.  
                XmlElement chat = XmlDoc.CreateElement("chat");


                // Create the Value.
                XmlText chatString = XmlDoc.CreateTextNode(chatLine);


                // Append value.
                chat.AppendChild(chatString);


                // Find the correct event and add the node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(chat);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }

        /// <summary>
        /// Writes a chat event to the xml this writer is assigned to
        /// Uses given event number
        /// </summary>
        /// <param name="eventNum">Integer that identifies the event</param>
        /// <param name="chatLine">String that is the message to be spoken</param>
        public void WriteChatToXml(int eventNum, string chatLine)
        {
            this.eventNumber = eventNum;

            try
            {
                // Create Xml node.  
                XmlElement chat = XmlDoc.CreateElement("chat");


                // Create the Value.
                XmlText chatString = XmlDoc.CreateTextNode(chatLine);


                // Append value.
                chat.AppendChild(chatString);


                // Find the correct event and add the node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(chat);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }

        /// <summary>
        /// Sets the event ID of the writer
        /// </summary>
        /// <param name="nID">Integer that is the event ID</param>
        public void setEventId(int nID)
        {
            this.eventNumber = nID;
        }
    }
}