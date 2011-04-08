//**************************************************************
// Class: BotChat
//
// Author: Joel McClain
//
// Date: 8-19-10
//
// Description: This class will out put normal chat into a virtual 
// world. Many options can be added to this using openmetaverse
// chat functions.
//***************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using OpenMetaverse;

namespace BotGUI
{
    class BotChat
    {
        #region Attributes

        GridClient client;

        #endregion

        #region Constructor

        public BotChat(GridClient client)
        {
            this.client = client;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method will allow a bot to chat in-world
        /// </summary>
        /// <param name="message">sentence that gets said in-world</param>
        public void loadChat(XmlTextReader reader)
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
