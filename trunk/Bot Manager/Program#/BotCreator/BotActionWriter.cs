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
        StopThread
    }

    class BotActionWriter
    {
        #region Attributes

        XmlDocument XmlDoc;
        int eventNumber;
        string botName;

        #endregion

        #region Constructor

        public BotActionWriter(XmlDocument XmlFile, int eventNum, string name)
        {
            this.XmlDoc = XmlFile;
            this.eventNumber = eventNum;
            this.botName = name;
        }

        #endregion

        #region Methods

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

        public void setEventId(int nID)
        {
            this.eventNumber = nID;
        }

        #endregion
    }
}
