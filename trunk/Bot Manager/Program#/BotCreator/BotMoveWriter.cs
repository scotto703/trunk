using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace BotGUI
{
    class BotMoveWriter
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
        public BotMoveWriter(XmlDocument XmlFile, int eventNum, string name)
        {
            this.XmlDoc = XmlFile;
            this.eventNumber = eventNum;
            this.botName = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes a movement event to the xml this writer is assigned to
        /// </summary>
        /// <param name="x">Double that is the x of a vector</param>
        /// <param name="y">Double that is the y of a vector</param>
        /// <param name="z">Double that is the z of a vector</param>
        public void WriteWalkToXml(double x, double y, double z)
        {
            try
            {
                // Create Xml nodes that are needed.  
                XmlElement movement = XmlDoc.CreateElement("movement");    // Outermost node
                XmlElement moveTo = XmlDoc.CreateElement("moveTo");        // Child node of movement
                XmlElement xNode = XmlDoc.CreateElement("x");              // Child node of moveTo 
                XmlElement yNode = XmlDoc.CreateElement("y");              // Child node of moveTo
                XmlElement zNode = XmlDoc.CreateElement("z");              // Child node of moveTo 

                // Create the Value (text) that each coordinate node will contain
                XmlText xCoord = XmlDoc.CreateTextNode(x.ToString());
                XmlText yCoord = XmlDoc.CreateTextNode(y.ToString());
                XmlText zCoord = XmlDoc.CreateTextNode(z.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                movement.AppendChild(moveTo);
                moveTo.AppendChild(xNode);
                moveTo.AppendChild(yNode);
                moveTo.AppendChild(zNode);

                // Append values of each coordinate node
                xNode.AppendChild(xCoord);
                yNode.AppendChild(yCoord);
                zNode.AppendChild(zCoord);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(movement);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }

        /// <summary>
        /// Writes a teleport event to the xml this writer is assigned to
        /// </summary>
        /// <param name="location">String that is the region name</param>
        /// <param name="x">Double that is the x of a vector</param>
        /// <param name="y">Double that is the y of a vector</param>
        /// <param name="z">Double that is the z of a vector</param>
        public void WriteTeleportToXml(string location, double x, double y, double z)
        {
            try
            {
                // Create Xml nodes needed.  
                XmlElement movement = XmlDoc.CreateElement("movement");    // Outermost node for a bot movement
                XmlElement teleport = XmlDoc.CreateElement("Teleport");    // Child node of movement
                XmlElement region = XmlDoc.CreateElement("Region");        // Child node of Teleport
                XmlElement vector = XmlDoc.CreateElement("Vector");        // Child node of Teleport
                XmlElement xNode = XmlDoc.CreateElement("x");              // Child node of Vector 
                XmlElement yNode = XmlDoc.CreateElement("y");              // Child node of Vector
                XmlElement zNode = XmlDoc.CreateElement("z");              // Child node of Vector

                // Put the region name into an Xml readable node
                XmlText regionName = XmlDoc.CreateTextNode(location);

                // Create the Value (text) that each coordinate node will contain
                XmlText xCoord = XmlDoc.CreateTextNode(x.ToString());
                XmlText yCoord = XmlDoc.CreateTextNode(y.ToString());
                XmlText zCoord = XmlDoc.CreateTextNode(z.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                movement.AppendChild(teleport);
                teleport.AppendChild(region);
                teleport.AppendChild(vector);
                vector.AppendChild(xNode);
                vector.AppendChild(yNode);
                vector.AppendChild(zNode);

                // Append the name of the region to corresponding xml node
                region.AppendChild(regionName);

                // Append values of each coordinate node
                xNode.AppendChild(xCoord);
                yNode.AppendChild(yCoord);
                zNode.AppendChild(zCoord);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(movement);
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

        #endregion
    }
}
