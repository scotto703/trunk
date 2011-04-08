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

        XmlDocument XmlDoc;
        int eventNumber;
        string botName;

        #endregion

        #region Constructor

        public BotChatWriter(XmlDocument XmlFile, int eventNum, string name)
        {
            this.XmlDoc = XmlFile;
            this.eventNumber = eventNum;
            this.botName = name;
        }

        #endregion


        public void WriteChatToXml(string chatLine)
        {
            try
            {
                // Create Xml node.  
                XmlElement chat = XmlDoc.CreateElement("Chat");


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

        public void WriteChatToXml(int eventNum, string chatLine)
        {
            this.eventNumber = eventNum;

            try
            {
                // Create Xml node.  
                XmlElement chat = XmlDoc.CreateElement("Chat");


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

        public void setEventId(int nID)
        {
            this.eventNumber = nID;
        }
    }
}