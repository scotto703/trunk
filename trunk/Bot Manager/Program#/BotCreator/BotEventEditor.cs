//**************************************************************
// Class: BotEventEditor
// 
// Author: Francisco Scovino
//
// Date: 11/21/2010
//
// Description: This class writes all the action methods into the
//              corresponding XML file             
//
//**************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace BotGUI
{
    class BotEventEditor
    {
        /// <summary>
        /// Name of the bot
        /// </summary>
        string bot;
        /// <summary>
        /// Event xml document
        /// </summary>
        XmlDocument eventDoc;
        /// <summary>
        /// Path or location of the event xml document
        /// </summary>
        string eventLoc;
        /// <summary>
        /// List of events that the bot may perform
        /// </summary>
        public List<Event> eventList;
        /// <summary>
        /// Action writer instance for writing to the event xml
        /// </summary>
        BotActionWriter actionWriter;
        /// <summary>
        /// Chat writer instance for writing to the event xml
        /// </summary>
        BotChatWriter chatWriter;
        /// <summary>
        /// Move writer instance for writing to the event xml
        /// </summary>
        BotMoveWriter moveWriter;

        /// <summary>
        /// load event xml file
        /// </summary>
        /// <param name="botName">String of the bot's name</param>
        public void loadBotEvents(string botName)
        {
            try
            {
                bot = botName;
                eventLoc = Environment.CurrentDirectory + "\\Bots\\" + bot + "\\Events\\events.xml";
                eventDoc = new XmlDocument();
                eventDoc.Load(eventLoc);

                loadEventList();
            }
            catch (Exception)
            {
                string message = "Error Reading Bot Event Xml File on Directory";
                string title = "Error Reading Xml File";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Return the event number and description of an especific event
        /// </summary>
        /// <param name="eventID">Integer that is the event number</param>
        public string getEventInfo(int eventID)
        {
            string eventInfo;
            eventInfo = "Event #" + eventList[eventID].getEventID() + ": " + eventList[eventID].getDescription();

            return eventInfo;
        }

        /// <summary>
        /// Return a TreeNode containing an event
        /// </summary>
        /// <param name="eventID">Integer that is the event number</param>
        /// <param name="tree">Treeview</param>
        public TreeNode getEventNodes(int eventID, TreeView tree)
        {
            TreeNode node;
            tree.Nodes.Clear();

            node = tree.Nodes.Add("Event #" + eventList[eventID].getEventID() + ":");
            node.Nodes.Add(eventList[eventID].getDescription());

            int i = 0;

            foreach (Node act in eventList[eventID].getListNodes())
            {
                switch (act.getActionNode())
                {
                    case "chat":
                        node.Nodes[0].Nodes.Add("BotChat: " + act.getChat());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Blue;
                        break;

                    case "moveTo":
                        node.Nodes[0].Nodes.Add("BotMove: Walk: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.ForestGreen;
                        break;

                    case "teleport":
                        node.Nodes[0].Nodes.Add("BotMove: Teleport: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.ForestGreen;
                        break;

                    case "animation":
                        node.Nodes[0].Nodes.Add("BotAction: Animation: " + act.getUUID());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "sit":
                        node.Nodes[0].Nodes.Add("BotAction: Sit UUID: " + act.getUUID() + " Vector: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "lookAt":
                        node.Nodes[0].Nodes.Add("BotAction: Turn Towards (Look at) Vector: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "stand":
                        node.Nodes[0].Nodes.Add("BotAction: Stand");
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "attachTo":
                        node.Nodes[0].Nodes.Add("BotAction: Attach object: " + act.getItemInv() + " Attach Point: " + act.getAttachPT());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "stopThread":
                        node.Nodes[0].Nodes.Add("BotAction: Sleep Bot " + act.getTimeSleep() + " (milliseconds)");
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "fly":
                        node.Nodes[0].Nodes.Add("BotAction: Fly/Land ");
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "clickObject":
                        node.Nodes[0].Nodes.Add("BotAction: Click on object: " + act.getLocalID());
                        node.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;
                }
            }

            return node;
        }

        /// <summary>
        /// Edit a current event with a new event
        /// The new event keeps the same event ID as the previous event
        /// </summary>
        /// <param name="newEvent">Event that is the new event</param>
        public void editEvent(Event newEvent)
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].getEventID() == newEvent.getEventID())
                {
                    eventList[i] = newEvent;
                }
            }
        }

        /// <summary>
        /// Insert an event before the selected event
        /// </summary>
        /// <param name="newEvent">Event that is the new event</param>
        public void insertEvent(Event newEvent)
        {
            if (newEvent.getEventID() < eventList.Count)
            {
                int j = 0;

                for (int i = (eventList.Count - 1); i >= newEvent.getEventID(); i--)
                {
                    j = i + 1;
                    eventList[i].setEventID(j);
                }

                eventList.Insert(newEvent.getEventID(), newEvent);
            }
        }

        /// <summary>
        /// Add a new Event at the bottom of an event list
        /// </summary>
        /// <param name="newEvent">Event that is the new event</param>
        public void addEvent(Event newEvent)
        {
            bool isRepeted = false;

            foreach (Event e in eventList)
            {
                if (newEvent.getEventID() == e.getEventID())
                {
                    System.Windows.Forms.MessageBox.Show("Event #: " + newEvent.getEventID() + " already exists");
                    isRepeted = true;
                }
            }

            if (isRepeted == false)
                eventList.Add(newEvent);
        }

        /// <summary>
        /// Remove the specified event from the event List
        /// </summary>
        /// <param name="newEvent">Integer that is the event ID</param>
        public void removeEvent(int eventID)
        {
            eventList.RemoveAt(eventID);
        }

        /// <summary>
        /// Create the events.xml file based on the list of events "eventList<Event>"
        /// </summary>
        public void saveXmlFile()
        {
            FileStream fs;
            XmlWriter w;

            //errase this and change variable below
            string eventLocat = Environment.CurrentDirectory + "\\Bots\\" + bot + "\\Events\\events.xml";

            try
            {
                fs = new FileStream(eventLocat, FileMode.Create);
                w = XmlWriter.Create(fs);

                w.WriteStartDocument();
                w.WriteStartElement("events");

                foreach (Event e in eventList)
                {
                    //create every event in the Xml file
                    w.WriteStartElement("event");
                    w.WriteAttributeString("ID", Convert.ToString(e.getEventID()));
                    w.WriteAttributeString("Name", e.getDescription());
                    w.WriteEndElement();
                }

                //close events' node and document
                w.WriteEndElement();
                w.WriteEndDocument();
                w.Flush();
                fs.Close();

            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                System.Windows.Forms.MessageBox.Show(message, "Error Creating Xml File", MessageBoxButtons.OK);
            }

            //insert action nodes inside each event node
            XmlDocument newEventDoc = new XmlDocument();
            newEventDoc.Load(eventLocat); //real name eventLoc, it must be changed

            chatWriter = new BotChatWriter(newEventDoc, 0, bot);
            moveWriter = new BotMoveWriter(newEventDoc, 0, bot);
            actionWriter = new BotActionWriter(newEventDoc, 0, bot);

            foreach (Event e in eventList)
            {
                chatWriter.setEventId(e.getEventID());
                moveWriter.setEventId(e.getEventID());
                actionWriter.setEventId(e.getEventID());

                foreach (Node n in e.getListNodes())
                {
                    switch (n.getActionNode())
                    {
                        case "chat":
                            chatWriter.WriteChatToXml(e.getEventID(), n.getChat());
                            break;

                        case "moveTo":
                            moveWriter.WriteWalkToXml(n.getValueX(), n.getValueY(), n.getValueZ());
                            break;

                        case "teleport":
                            moveWriter.WriteTeleportToXml(n.getRegion(), n.getValueX(), n.getValueY(), n.getValueZ());
                            break;

                        case "animation":
                            actionWriter.WriteAnimationActionToXml(n.getUUID(), n.getTime());
                            break;

                        case "sit":
                            actionWriter.WriteSitActionToXml(n.getUUID(), n.getValueX(), n.getValueY(), n.getValueZ(), n.getTime());
                            break;

                        case "lookAt":
                            actionWriter.WriteLookAtActionToXml(n.getValueX(), n.getValueY(), n.getValueZ(), n.getTime());
                            break;

                        case "stand":
                            actionWriter.WriteStandActionToXml(n.getTime());
                            break;

                        case "attachTo":
                            actionWriter.WriteAttachToActionToXml(n.getItemInv(), n.getAttachPT(), n.getTime());
                            break;

                        case "stopThread":
                            actionWriter.WriteStopThreadActionToXml(n.getTimeSleep(), n.getTime());
                            break;

                        case "fly":
                            actionWriter.WriteFlyActionToXml();
                            break;

                        case "clickObject":
                            actionWriter.WriteClickObjectActionToXml(n.getLocalID());
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Load events from xml to List<Event> eventList
        /// </summary>
        private void loadEventList()
        {
            eventList = new List<Event>();

            try
            {
                //
                // Check to make sure the root element is not empty
                //
                if (eventDoc.DocumentElement.ChildNodes.Count == 0)
                {
                    Exception noEventsException = new Exception();
                    throw noEventsException;
                }

                //
                // Root element is not empty, so copy the file into list
                //
                // XmlNode nList = eventDoc.FirstChild.ChildNodes;

                foreach (XmlNode nodeEvent in eventDoc.DocumentElement.ChildNodes)
                {
                    int eventID = 0;
                    string eventName = "";

                    eventID = Int32.Parse(nodeEvent.Attributes["ID"].Value);
                    eventName = nodeEvent.Attributes["Name"].Value;
                    List<Node> listNodes = new List<Node>();

                    //XmlNodeList nAction = nodeEvent.ChildNodes;
                    foreach (XmlNode nodeAction in nodeEvent.ChildNodes)
                    {
                        double xVal = 0;
                        double yVal = 0;
                        double zVal = 0;
                        string uuid = "";
                        uint localID = 0;
                        string iItem = "";
                        string aPoint = "";
                        int sleepTime = 0;
                        int timer = 0;
                        int actionType = 0;
                        string region = "";
                        string chat = "";

                        switch (nodeAction.Name)
                        {
                            case "movement":
                                {
                                    XmlNode nodeMove = nodeAction.FirstChild;

                                    if (nodeMove.Name == "Teleport" || nodeMove.Name == "teleport")
                                    {
                                        foreach (XmlNode node1 in nodeMove.ChildNodes)
                                        {
                                            if (node1.Name == "Region" || node1.Name == "region")
                                                region = node1.InnerText; // Convert.ToString(node1.Value)
                                            else
                                            {
                                                xVal = Double.Parse(node1.ChildNodes.Item(0).FirstChild.Value);
                                                yVal = Double.Parse(node1.ChildNodes.Item(1).FirstChild.Value);
                                                zVal = Double.Parse(node1.ChildNodes.Item(2).FirstChild.Value);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        xVal = Double.Parse(nodeMove.ChildNodes.Item(0).FirstChild.Value);
                                        yVal = Double.Parse(nodeMove.ChildNodes.Item(1).FirstChild.Value);
                                        zVal = Double.Parse(nodeMove.ChildNodes.Item(2).FirstChild.Value);
                                    }
                                    break;
                                }

                            case "action":
                                {
                                    XmlNode nodeCreateAction = nodeAction.FirstChild;

                                    foreach (XmlNode node1 in nodeCreateAction.ChildNodes)
                                    {
                                        if (node1.Name == "Vector")
                                        {
                                            xVal = Double.Parse(node1.ChildNodes.Item(0).InnerText);
                                            yVal = Double.Parse(node1.ChildNodes.Item(1).InnerText);
                                            zVal = Double.Parse(node1.ChildNodes.Item(2).InnerText);
                                        }
                                        else if (node1.Name == "SleepTime")
                                            sleepTime = Int32.Parse(node1.InnerText);
                                        else if (node1.Name == "actionType")
                                            actionType = Int32.Parse(node1.InnerText);
                                        else if (node1.Name == "UUID")
                                            uuid = node1.InnerText;
                                        else if (node1.Name == "localID")
                                            localID = uint.Parse(node1.InnerText);
                                        else if (node1.Name == "InvItem")
                                            iItem = node1.InnerText;
                                        else if (node1.Name == "AttachPoint")
                                            aPoint = node1.InnerText;
                                        else
                                            timer = Int32.Parse(node1.InnerText);
                                    }
                                    break;
                                }
                            // 
                            case "Chat":
                                {
                                    chat = nodeAction.FirstChild.Value;
                                    break;
                                }
                            case "chat":
                                {
                                    chat = nodeAction.FirstChild.Value;
                                    break;
                                }
                        }
                        Node newNode = new Node(actionType, sleepTime, timer, xVal, yVal, zVal, region, chat, uuid, localID, iItem, aPoint);
                        listNodes.Add(newNode);
                    }

                    Event newEvent = new Event(eventID, eventName, listNodes);
                    eventList.Add(newEvent);
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There are no events for the chosen bot", "Loading Events", MessageBoxButtons.OK);
            }
        }
    }

    #region Event Class
    public class Event
    {
        int id;
        string description;
        List<Node> node;

        public Event(int nID, string nDesc, List<Node> nNode)
        {
            id = nID;
            description = nDesc;
            node = new List<Node>(nNode);
        }

        public int getEventID()
        {
            return id;
        }

        public string getDescription()
        {
            return description;
        }

        public List<Node> getListNodes()
        {
            return node;
        }

        public void setEventID(int newID)
        {
            this.id = newID;
        }
    }
    #endregion

    #region Node Class
    public class Node
    {
        string action;
        int actionType;
        string UUID;
        uint localID;
        string invItem;
        string attachPT;
        int timeSleep;
        int time;
        double x, y, z;
        string region;
        string chat;

        public Node(int nActionType, int nTimeSleep, int nTime, double nX, double nY, double nZ, string nRegion, string nChat, string nUUID, uint nLocalID, string nInvItem, string nAttachPT)
        {
            actionType = nActionType;
            timeSleep = nTimeSleep;
            time = nTime;
            x = nX;
            y = nY;
            z = nZ;
            region = nRegion;
            chat = nChat;
            action = "";
            UUID = nUUID;
            localID = nLocalID;
            invItem = nInvItem;
            attachPT = nAttachPT;

            setAction();
        }

        void setAction()
        {
            if ((this.x != 0) || (this.y != 0) || (this.z != 0))
            {
                if (this.actionType == 3)
                    this.action = "lookAt";
                else if (this.region == "")
                    this.action = "moveTo";
                else
                    this.action = "teleport";
            }
            else if (this.chat != "")
                this.action = "chat";
            else
            {
                switch (this.actionType)
                {
                    case 1:
                        this.action = "animation";
                        break;
                    case 2:
                        this.action = "sit";
                        break;
                    case 3:
                        this.action = "lookAt";
                        break;
                    case 4:
                        this.action = "stand";
                        break;
                    case 5:
                        this.action = "attachTo";
                        break;
                    case 6:
                        this.action = "stopThread";
                        break;
                    case 7:
                        this.action = "fly";
                        break;
                    case 8:
                        this.action = "clickObject";
                        break;
                }
            }
        }

        public string getActionNode()
        {
            return action;
        }

        public void setActionNode(string act)
        {
            action = act;
        }

        public int getActionType()
        {
            return actionType;
        }

        public void setActionType(int actType)
        {
            actionType = actType;
        }

        public int getTimeSleep()
        {
            return timeSleep;
        }

        public void setTimeSleep(int tSleep)
        {
            timeSleep = tSleep;
        }

        public int getTime()
        {
            return time;
        }

        public void setTime(int nTime)
        {
            time = nTime;
        }

        public double getValueX()
        {
            return x;
        }

        public void setValueX(double nX)
        {
            x = nX;
        }

        public double getValueY()
        {
            return y;
        }

        public void setValueY(double nY)
        {
            y = nY;
        }

        public double getValueZ()
        {
            return z;
        }

        public void setValueZ(double nZ)
        {
            z = nZ;
        }

        public string getRegion()
        {
            return region;
        }

        public void setRegion(string reg)
        {
            region = reg;
        }

        public string getChat()
        {
            return chat;
        }

        public void setChat(string nChat)
        {
            chat = nChat;
        }

        public string getUUID()
        {
            return UUID;
        }

        public void setUUID(string nUUID)
        {
            UUID = nUUID;
        }

        public uint getLocalID()
        {
            return localID;
        }

        public void setLocalID(uint nLocalID)
        {
            localID = nLocalID;
        }

        public string getItemInv()
        {
            return invItem;
        }

        public void setItemInv(string iItem)
        {
            invItem = iItem;
        }

        public string getAttachPT()
        {
            return attachPT;
        }

        public void setAttachPT(string aPoint)
        {
            attachPT = aPoint;
        }
    }
    #endregion
}