//**************************************************************
// Class: XmlBotList
//
// Author: Lawrence Miller
//
// Description: This class creates an object for a list of bots
//
//
//**************************************************************

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace BotGUI
{
    class XmlBotList
    {
        #region Attributes

        /// <summary>
        /// Holds the directory path for bots
        /// </summary>
        internal string botPath;

        /// <summary>
        ///
        /// </summary>
        private DirectoryInfo BotDirectory = null;

        private DirectoryInfo[] BotList = null;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Default constructor sets to default bot path
        /// </summary>
        public XmlBotList()
        {
            // set default bot path
            botPath = Directory.GetCurrentDirectory() + "\\Bots";

            //load bot list
            LoadList();
        }

        /// <summary>
        /// Constructor for specifying own both path
        /// </summary>
        /// <param name="path">Directory path containing bots folders</param>
        public XmlBotList(string path)
        {
            try
            {
                //set bot path specified
                botPath = path;

                //load bot list
                LoadList();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show(@"Bot Path Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates a list of all bots in the path specified.
        /// Saves the file under BotList.Xml.
        /// </summary>
        /// <returns>Void</returns>
        public void CreateXml()
        {
            XmlTextWriter writer = new XmlTextWriter(BotDirectory.ToString() + "\\BotList.Xml", null);

            //Set Xml file indentation
            writer.Formatting = Formatting.Indented;

            //Create XML BostList.Xml file
            writer.WriteStartDocument();

            //Start Root Element
            writer.WriteStartElement("Bots");

            for (int counter = 0; counter < BotList.Length; counter++)
            {
                //Add bots to XML file, Omit Hidden Directories
                if (!BotList[counter].Attributes.ToString().Contains("Hidden"))
                {
                    //Add bot element and name to Xml file
                    writer.WriteStartElement("bot");
                    writer.WriteString(BotList[counter].Name);
                    writer.WriteEndElement();
                }
            }

            //Close root element
            writer.WriteEndElement();

            //Close Xml file
            writer.Close();
        }

        /// <summary>
        /// Method to find a bot in the list.  Returns true if bot found.
        /// </summary>
        /// <param name="botName">Name of the bot to find</param>
        /// <returns></returns>
        public Boolean BotFound(String botName)
        {
            var reader = new XmlTextReader(BotDirectory.ToString() + @"\BotList.Xml");

            while (reader.Read())
            {
                //If the XMLNode is not an element skip to the next loop iteration
                if (reader.NodeType != XmlNodeType.Element) continue;

                //If the XML element name is not Bot, skip to the next loop iteration
                if (reader.Name != "Bot") continue;

                //Compare the current element string to botName and return true if there is a match
                if (System.String.Compare(reader.ReadElementString(), botName, true) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Loads the bot list.
        /// </summary>
        private void LoadList()
        {
            //gets a list for files in the directory
            BotDirectory = new DirectoryInfo(botPath);
            BotList = BotDirectory.GetDirectories();
        }

        #endregion Methods
    }
}