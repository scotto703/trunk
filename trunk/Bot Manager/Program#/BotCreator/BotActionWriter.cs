//**************************************************************
// Class: BotActionWriter
// 
// Author: Francisco Scovino
//
// Date: 01/29/2011
//
// Description: This class writes all the action methods into the
//              corresponding XML file             
//
//**************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace BotGUI
{
    enum ActionType
    {
        Animation = 1,
        Sit,
        LookAt,
        Stand,
        AttachTo,
        StopThread,
        Fly,
        ClickObject
    }

    class BotActionWriter
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
        public BotActionWriter(XmlDocument XmlFile, int eventNum, string name)
        {
            this.XmlDoc = XmlFile;
            this.eventNumber = eventNum;
            this.botName = name;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Writes an animation action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="uuid">String that is the universal unique ID of the action</param>
        /// <param name="time">Integer that is the length of time to perform the animation</param>
        public void WriteAnimationActionToXml(string uuid, int time)
        {
            int actType = (int)ActionType.Animation;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement uuID = XmlDoc.CreateElement("UUID");                     // Child node of createAction
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText strUUID = XmlDoc.CreateTextNode(uuid.ToString());
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(uuID);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                uuID.AppendChild(strUUID);
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }

        }
        /// <summary>
        /// Writes a sit action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="uuid">String that is the universal unique ID of the action</param>
        /// <param name="x">Double that is the x value of a vector</param>
        /// <param name="y">Double that is the y value of a vector</param>
        /// <param name="z">Double that is the z value of a vector</param>
        /// <param name="time">Integer, not used by the sit event</param>
        public void WriteSitActionToXml(string uuid, double x, double y, double z, int time)
        {
            int actType = (int)ActionType.Sit;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement uuID = XmlDoc.CreateElement("UUID");                     // Child node of createAction
                XmlElement vector = XmlDoc.CreateElement("Vector");                 // Child node of createAction
                XmlElement xNode = XmlDoc.CreateElement("x");                       // Child node of vector
                XmlElement yNode = XmlDoc.CreateElement("y");                       // Child node of vector
                XmlElement zNode = XmlDoc.CreateElement("z");                       // Child node of vector
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText strUUID = XmlDoc.CreateTextNode(uuid.ToString());
                XmlText xCoord = XmlDoc.CreateTextNode(x.ToString());
                XmlText yCoord = XmlDoc.CreateTextNode(y.ToString());
                XmlText zCoord = XmlDoc.CreateTextNode(z.ToString());
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(uuID);
                createAction.AppendChild(vector);
                vector.AppendChild(xNode);
                vector.AppendChild(yNode);
                vector.AppendChild(zNode);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                uuID.AppendChild(strUUID);
                xNode.AppendChild(xCoord);
                yNode.AppendChild(yCoord);
                zNode.AppendChild(zCoord);
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }
        /// <summary>
        /// Writes a look action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="x">Double that is the x value of a vector</param>
        /// <param name="y">Double that is the y value of a vector</param>
        /// <param name="z">Double that is the z value of a vector</param>
        /// <param name="time">Integer, not used by the look event</param>
        public void WriteLookAtActionToXml(double x, double y, double z, int time)
        {
            int actType = (int)ActionType.LookAt;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction
                XmlElement vector = XmlDoc.CreateElement("Vector");                 // Child node of createAction
                XmlElement xNode = XmlDoc.CreateElement("x");                       // Child node of vector
                XmlElement yNode = XmlDoc.CreateElement("y");                       // Child node of vector
                XmlElement zNode = XmlDoc.CreateElement("z");                       // Child node of vector

                // Put arguments into an Xml readable node
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());
                XmlText xCoord = XmlDoc.CreateTextNode(x.ToString());
                XmlText yCoord = XmlDoc.CreateTextNode(y.ToString());
                XmlText zCoord = XmlDoc.CreateTextNode(z.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(vector);
                vector.AppendChild(xNode);
                vector.AppendChild(yNode);
                vector.AppendChild(zNode);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                xNode.AppendChild(xCoord);
                yNode.AppendChild(yCoord);
                zNode.AppendChild(zCoord);
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);


                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }

        }
        /// <summary>
        /// Writes a stand action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="time">Integer, not used by the stand event</param>
        public void WriteStandActionToXml(int time)
        {
            int actType = (int)ActionType.Stand;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }
        /// <summary>
        /// Writes an attach action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="iItem">String that identifies the item</param>
        /// <param name="attachPT">String that identifies the attach point</param>
        /// <param name="time">Integer, not used by the attach event</param>
        public void WriteAttachToActionToXml(string iItem, string attachPT, int time)
        {
            int actType = (int)ActionType.AttachTo;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement invItem = XmlDoc.CreateElement("InvItem");               // Child node of createAction
                XmlElement attachPoint = XmlDoc.CreateElement("AttachPoint");       // Child node of createAction
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText strInvItem = XmlDoc.CreateTextNode(iItem.ToString());
                XmlText strAttachPT = XmlDoc.CreateTextNode(attachPT.ToString());
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(invItem);
                createAction.AppendChild(attachPoint);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                invItem.AppendChild(strInvItem);
                attachPoint.AppendChild(strAttachPT);
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }
        /// <summary>
        /// Writes a sleep thread action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="sleep">Integer that is the sleep time in milliseconds</param>
        /// <param name="time">Integer, not used by the attach event</param>
        public void WriteStopThreadActionToXml(int sleep, int time)
        {
            int actType = (int)ActionType.StopThread;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement sleepTime = XmlDoc.CreateElement("SleepTime");           // Child node of createAction
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction
                XmlElement timer = XmlDoc.CreateElement("Timer");                   // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText intSleep = XmlDoc.CreateTextNode(sleep.ToString());
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());
                XmlText intTime = XmlDoc.CreateTextNode(time.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(sleepTime);
                createAction.AppendChild(actionType);
                createAction.AppendChild(timer);

                // Append values to corresponding xml node
                sleepTime.AppendChild(intSleep);
                actionType.AppendChild(intAction);
                timer.AppendChild(intTime);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }
        /// <summary>
        /// Writes a toggle fly action event to the xml this writer is assigned to
        /// </summary>
        public void WriteFlyActionToXml()
        {
            int actType = (int)ActionType.Fly;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(actionType);

                // Append values to corresponding xml node
                actionType.AppendChild(intAction);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
                }

                XmlDoc.Save(Environment.CurrentDirectory + "\\Bots\\" + botName + "\\Events\\events.xml");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Failed to update XML file");
            }
        }
        /// <summary>
        /// Writes a click object action event to the xml this writer is assigned to
        /// </summary>
        /// <param name="uuid">
        /// String that is the id of the object. 
        /// Reusing uuid instead of creating new variables for id
        /// </param> 
        public void WriteClickObjectActionToXml(uint lID)
        {
            int actType = (int)ActionType.ClickObject;

            try
            {
                // Create Xml nodes that are needed
                XmlElement action = XmlDoc.CreateElement("action");                 // Outermost node
                XmlElement createAction = XmlDoc.CreateElement("createAction");     // Child node of action
                XmlElement localID = XmlDoc.CreateElement("localID");                     // Child node of createAction
                XmlElement actionType = XmlDoc.CreateElement("actionType");         // Child node of createAction

                // Put arguments into an Xml readable node
                XmlText strLocalID = XmlDoc.CreateTextNode(lID.ToString());
                XmlText intAction = XmlDoc.CreateTextNode(actType.ToString());

                // Append nodes to each other, from the outermost node to innermost node
                action.AppendChild(createAction);
                createAction.AppendChild(localID);
                createAction.AppendChild(actionType);

                // Append values to corresponding xml node
                localID.AppendChild(strLocalID);
                actionType.AppendChild(intAction);

                // Find the correct event and add the completed node to it
                foreach (XmlNode node in XmlDoc.DocumentElement.ChildNodes)
                {
                    if (node.Attributes["ID"].Value == eventNumber.ToString())
                        node.AppendChild(action);
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
