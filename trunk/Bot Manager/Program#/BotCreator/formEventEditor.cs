//**************************************************************
// Class: formEventsEditor
// 
// Author: Francisco Scovino
//
// Date: 02/23/2011
//
// Description: This form edits bot's events. It modifies, adds,
//              and deletes actions from a specific event             
//
//**************************************************************

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BotGUI
{
    public partial class formEventEditor : Form
    {
        #region Attributes
        /// <summary>
        /// Writes action events to xml
        /// </summary>
        BotEventEditor be;
        /// <summary>
        /// Event ID that the form is showing
        /// </summary>
        int eventID;
        /// <summary>
        /// Description of the event that was given during its creation
        /// </summary>
        string eventDescription;
        /// <summary>
        /// Instance of the event class. Found in BotEventEditor.cs
        /// </summary>
        Event singleEvent;
        /// <summary>
        /// Instance of the node class. Found in BotEventEditor.cs
        /// </summary
        List<Node> nodes;
        /// <summary>
        /// Used by save button to determine if it should offer to 
        /// insert or append 
        /// </summary>
        bool isNewEvent;
        /// <summary>
        /// Used when updating fields on the form
        /// </summary>
        bool isSameAction;
        /// <summary>
        /// Used when updating fields on the form to display information
        /// about the currently selected node
        /// </summary>
        int currentNode;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public formEventEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for editing an existing event
        /// </summary>
        /// <param name="botName">String that is the bot's name</param>
        /// <param name="eID">Integer that identifies the event show by the form</param>
        public formEventEditor(string botName, int eID)
        {
            InitializeComponent();
            be = new BotEventEditor();
            be.loadBotEvents(botName);
            eventID = eID;
            eventDescription = be.eventList[eID].getDescription();
            isNewEvent = false;
            nodes = new List<Node>();
            nodes = be.eventList[eID].getListNodes();
            resetAllFields();

            if (nodes.Count == 0)
            {
                string infoNewNode = "This event has no actions, you can beging adding actions by modifying this Chat action";
                Node emptyNode = new Node(0, 0, 0, 0.0, 0.0, 0.0, "", "", infoNewNode, 0, "", "");
                nodes.Add(emptyNode);
            }

            setEventInfo();
        }

        #endregion

        #region Form Buttons

        private void btn_clear_Click(object sender, EventArgs e)
        {
            resetAllFields();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            isNewEvent = false;
            int actionID = treeView.SelectedNode.Index;
            currentNode = treeView.SelectedNode.Index;
            string action = nodes[actionID].getActionNode();
            isSameAction = false;
            setFields(action);
        }

        private void cbo_actionList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void cbo_actionList_Leave(object sender, EventArgs e)
        {
            string action = cbo_actionList.Text;
            isSameAction = true;
            setFields(action);
            cbo_actionList.Text = action;
        }

        private void btn_newAction_Click(object sender, EventArgs e)
        {
            isNewEvent = true;
            resetAllFields();
            cbo_actionList.Enabled = true;
            cbo_actionList.Focus();
        }

        private void btn_deleteAction_Click(object sender, EventArgs e)
        {
            nodes.Remove(nodes[currentNode]);
            
            singleEvent = new Event(eventID, eventDescription, nodes);
            be.editEvent(singleEvent);
            be.saveXmlFile();
            
            updateTreeView();
            treeView.ExpandAll();
            resetAllFields();
            //treeView.Focus();
            
        }

        private void btn_saveAction_Click(object sender, EventArgs e)
        {
            if (isNewEvent)
            {
                Node newNode = new Node(0, 0, 0, 0.0, 0.0, 0.0, "", "", "", 0, "", "");

                string msg = "Do you want to Insert a new action in the position selected? To Insert press YES," +
                        " to append a new action press NO";

                DialogResult insert = MessageBox.Show(msg, "Insert or Append Action", MessageBoxButtons.YesNoCancel);

                if (insert == DialogResult.Yes)
                {
                    nodes.Insert(currentNode, newNode);
                }
                else if (insert == DialogResult.No)
                {
                    nodes.Add(newNode);
                    currentNode = nodes.Count - 1;
                }
            }

            saveAction(cbo_actionList.Text);
            
            singleEvent = new Event(eventID, eventDescription, nodes);
            be.editEvent(singleEvent);
            be.saveXmlFile();
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            /*singleEvent = new Event(eventID, eventDescription, nodes);
            be.editEvent(singleEvent);
            be.saveXmlFile();*/
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Edit Events Methods

        /// <summary>
        /// reset all fields to its initial state
        /// </summary>
        private void resetAllFields()
        {
            cbo_actionList.Enabled = false;
            cbo_actionList.Text = "";

            txt_chat.Enabled = false;
            txt_chat.Text = "";

            txt_UUID.Enabled = false;
            txt_UUID.Text = "";

            txt_itemInv.Enabled = false;
            txt_itemInv.Text = "";

            cbo_attachPT.Enabled = false;
            cbo_attachPT.Text = "";

            txt_region.Enabled = false;
            txt_region.Text = "";

            num_valueX.Enabled = false;
            num_valueX.Value = 0;

            num_valueY.Enabled = false;
            num_valueY.Value = 0;

            num_valueZ.Enabled = false;
            num_valueZ.Value = 0;

            txt_sleepTime.Enabled = false;
            txt_sleepTime.Text = "";

            txt_actionType.Enabled = false;
            txt_actionType.Text = "";

            txt_timer.Enabled = false;
            txt_timer.Text = "";
        }
        
        
        /// <summary>
        /// set fields on form according to the action selected on the treeView
        /// </summary>
        /// <param name="act">String that identifies the action to configure fields to</param>
        private void setFields(string act)
        {
            switch (act)
            {
                case "lookAt":
                    setFieldsLookAt();
                    break;
                case "moveTo":
                    setFieldMoveTo();
                    break;
                case "teleport":
                    setFieldteleport();
                    break;
                case "chat":
                    setFieldChat();
                    break;
                case "animation":
                    setFieldAnimation();
                    break;
                case "sit":
                    setFieldSit();
                    break;
                case "stand":
                    setFieldStand();
                    break;
                case "attachTo":
                    setFieldAttachTo();
                    break;
                case "stopThread":
                    setFieldSleep();
                    break;
                case "fly":
                    setFieldFly();
                    break;
                case "clickObject":
                    setFieldClickObject();
                    break;
            }
        }

        /// <summary>
        /// Save the current action in the event
        /// </summary>
        /// <param name="act">String that identifies the action to call the correct save function</param>
        private void saveAction(string act)
        {
            switch (act)
            {
                case "lookAt":
                    saveActionLookAt();
                    break;
                case "moveTo":
                    saveActionMoveTo();
                    break;
                case "teleport":
                    saveActionteleport();
                    break;
                case "chat":
                    saveActionChat();
                    break;
                case "animation":
                    saveActionAnimation();
                    break;
                case "sit":
                    saveActionSit();
                    break;
                case "stand":
                    saveActionStand();
                    break;
                case "attachTo":
                    saveActionAttachTo();
                    break;
                case "stopThread":
                    saveActionSleep();
                    break;
                case "fly":
                    saveActionFly();
                    break;
                case "clickObject":
                    saveActionClickObject();
                    break;
            }
        }

        /// <summary>
        /// Set the event ID and Description from the selected event on the form
        /// it also sets the treeView with all actions belonging to the current event
        /// </summary>
        private void setEventInfo()
        {
            txt_eventID.Text = eventID.ToString();
            txt_eventDescription.Text = eventDescription;
            txt_eventDescription.Enabled = true;
            updateTreeView();
            treeView.ExpandAll();
        }

        /// <summary>
        /// Enable fields for action LookAt
        /// </summary>
        private void setFieldsLookAt()
        {
            resetAllFields();
            
            cbo_actionList.Enabled = true;
            num_valueX.Enabled = true;
            num_valueY.Enabled = true;
            num_valueZ.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;
            

            if (isSameAction)
            {
                num_valueX.Value = 0;
                num_valueY.Value = 0;
                num_valueZ.Value = 0;
                txt_actionType.Text = "3";
                txt_timer.Text = "0";
                num_valueX.Focus();
                
            }
            else
            {
                int i = currentNode;
                
                cbo_actionList.Text = nodes[i].getActionNode();
                
                num_valueX.Value = (decimal)nodes[i].getValueX();
                num_valueY.Value = (decimal)nodes[i].getValueY();
                num_valueZ.Value = (decimal)nodes[i].getValueZ();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to lookat event parameters
        /// </summary>
        private void saveActionLookAt()
        {
            int i = currentNode;
            
            try
            {
                double x = (double)num_valueX.Value, y = (double)num_valueY.Value, z = (double)num_valueZ.Value;
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setValueX(x);
                nodes[i].setValueY(y);
                nodes[i].setValueZ(z);
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the number you entered: " + e.Message;
                string caption = "Numeric Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action MoveTo
        /// </summary>
        private void setFieldMoveTo()
        {
            resetAllFields();
            
            cbo_actionList.Enabled = true;
            num_valueX.Enabled = true;
            num_valueY.Enabled = true;
            num_valueZ.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                num_valueX.Value = 0;
                num_valueY.Value = 0;
                num_valueZ.Value = 0;
                num_valueX.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                num_valueX.Value = (decimal)nodes[i].getValueX();
                num_valueY.Value = (decimal)nodes[i].getValueY();
                num_valueZ.Value = (decimal)nodes[i].getValueZ();
            }
        }

        /// <summary>
        /// Updates the current node according to moveto event parameters
        /// </summary>
        private void saveActionMoveTo()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setValueX(Double.Parse(num_valueX.Text));
                nodes[i].setValueY(Double.Parse(num_valueY.Text));
                nodes[i].setValueZ(Double.Parse(num_valueZ.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the number you entered: " + e.Message;
                string caption = "Numeric Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Enable fields for action teleport
        /// </summary>
        private void setFieldteleport()
        {
            setFieldMoveTo();
            txt_region.Enabled = true;

            if (isSameAction)
            {
                txt_region.Text = "Baker Island";
                txt_region.Focus();
            }
            else
            {
                int j = currentNode;
                txt_region.Text = nodes[j].getRegion();
            }
        }

        /// <summary>
        /// Updates the current node according to teleport event parameters
        /// </summary>
        private void saveActionteleport()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setRegion(txt_region.Text);
                nodes[i].setValueX(Double.Parse(num_valueX.Text));
                nodes[i].setValueY(Double.Parse(num_valueY.Text));
                nodes[i].setValueZ(Double.Parse(num_valueZ.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the number you entered: " + e.Message;
                string caption = "Numeric Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action Chat
        /// </summary>
        private void setFieldChat()
        {
            resetAllFields();
            
            cbo_actionList.Enabled = true;
            txt_chat.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_chat.Text = "";
                txt_chat.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_chat.Text = nodes[i].getChat();
            }
        }

        /// <summary>
        /// Updates the current node according to chat event parameters
        /// </summary>
        private void saveActionChat()
        {
            int i = currentNode;

            nodes[i].setActionNode(cbo_actionList.Text);
            nodes[i].setChat(txt_chat.Text);

            updateTreeView();
            treeView.ExpandAll();
        }

        /// <summary>
        /// Enable fields for action animation
        /// </summary>
        private void setFieldAnimation()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_UUID.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_UUID.Text = "";
                txt_actionType.Text = "1";
                txt_timer.Text = "0";
                txt_UUID.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_UUID.Text = nodes[i].getUUID();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to animation event parameters
        /// </summary>
        private void saveActionAnimation()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setUUID(txt_UUID.Text);
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action Sit
        /// </summary>
        private void setFieldSit()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_UUID.Enabled = true;
            num_valueX.Enabled = true;
            num_valueY.Enabled = true;
            num_valueZ.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_UUID.Text = "";
                num_valueX.Text = "0";
                num_valueY.Text = "0";
                num_valueZ.Text = "0";
                txt_actionType.Text = "2";
                txt_timer.Text = "0";
                txt_UUID.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_UUID.Text = nodes[i].getUUID();
                num_valueX.Text = nodes[i].getValueX().ToString();
                num_valueY.Text = nodes[i].getValueY().ToString();
                num_valueZ.Text = nodes[i].getValueZ().ToString();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to sit event parameters
        /// </summary>
        private void saveActionSit()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setUUID(txt_UUID.Text);
                nodes[i].setValueX(Double.Parse(num_valueX.Text));
                nodes[i].setValueY(Double.Parse(num_valueY.Text));
                nodes[i].setValueZ(Double.Parse(num_valueZ.Text));
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action Stand
        /// </summary>
        private void setFieldStand()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_actionType.Text = "4";
                txt_timer.Text = "0";
                txt_timer.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to stand event parameters
        /// </summary>
        private void saveActionStand()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action AttachTo
        /// </summary>
        private void setFieldAttachTo()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_itemInv.Enabled = true;
            cbo_attachPT.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_itemInv.Text = "";
                cbo_attachPT.Text = "Default";
                txt_actionType.Text = "5";
                txt_timer.Text = "0";
                txt_itemInv.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_itemInv.Text = nodes[i].getItemInv();
                cbo_attachPT.Text = nodes[i].getAttachPT();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to attachto event parameters
        /// </summary>
        private void saveActionAttachTo()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setItemInv(txt_itemInv.Text);
                nodes[i].setAttachPT(cbo_attachPT.Text);
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action SleepBot
        /// </summary>
        private void setFieldSleep()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_sleepTime.Enabled = true;
            txt_timer.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_sleepTime.Text = "0";
                txt_actionType.Text = "6";
                txt_timer.Text = "0";
                txt_sleepTime.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_sleepTime.Text = nodes[i].getTimeSleep().ToString();
                txt_actionType.Text = nodes[i].getActionType().ToString();
                txt_timer.Text = nodes[i].getTime().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to sleep event parameters
        /// </summary>
        private void saveActionSleep()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setTimeSleep(Int32.Parse(txt_sleepTime.Text));
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));
                nodes[i].setTime(Int32.Parse(txt_timer.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action fly/land
        /// </summary>
        private void setFieldFly()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_actionType.Text = "7";
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_actionType.Text = nodes[i].getActionType().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to fly/land event parameters
        /// </summary>
        private void saveActionFly()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "User Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Enable fields for action click object
        /// The click object action is currently disabled
        /// Add "clickObject" to combo box to re-enable
        /// Consider creating new text box and label for this instead of reusing UUID
        /// </summary>
        private void setFieldClickObject()
        {
            resetAllFields();

            cbo_actionList.Enabled = true;
            txt_UUID.Enabled = true;
            btn_saveAction.Enabled = true;

            if (isSameAction)
            {
                txt_UUID.Text = "";
                txt_actionType.Text = "8";
                txt_UUID.Focus();
            }
            else
            {
                int i = currentNode;

                cbo_actionList.Text = nodes[i].getActionNode();
                txt_UUID.Text = nodes[i].getLocalID().ToString();
                txt_actionType.Text = nodes[i].getActionType().ToString();
            }
        }

        /// <summary>
        /// Updates the current node according to click object event parameters
        /// </summary>
        private void saveActionClickObject()
        {
            int i = currentNode;

            try
            {
                nodes[i].setActionNode(cbo_actionList.Text);
                nodes[i].setLocalID(uint.Parse(txt_UUID.Text));//uses uuid text box, consider changing later
                nodes[i].setActionType(Int32.Parse(txt_actionType.Text));

                updateTreeView();
                treeView.ExpandAll();
            }
            catch (Exception e)
            {
                string message = "There was a problem with the values you entered: " + e.Message;
                string caption = "Numeric Error";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Update the information contained in the treeView
        /// </summary>
        private void updateTreeView()
        {
            TreeNode node1;
            treeView.Nodes.Clear();

            node1 = treeView.Nodes.Add("Event #" + eventID + ":");
            node1.Nodes.Add(eventDescription);

            int i = 0;
            
            foreach (Node act in nodes)
            {
                switch (act.getActionNode())
                {
                    case "chat":
                        node1.Nodes[0].Nodes.Add("BotChat: " + act.getChat());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Blue;
                        break;

                    case "moveTo":
                        node1.Nodes[0].Nodes.Add("BotMove: Walk: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.ForestGreen;
                        break;

                    case "teleport":
                        node1.Nodes[0].Nodes.Add("BotMove: teleport: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.ForestGreen;
                        break;

                    case "animation":
                        node1.Nodes[0].Nodes.Add("BotAction: Animation: " + act.getUUID());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "sit":
                        node1.Nodes[0].Nodes.Add("BotAction: Sit UUID: " + act.getUUID() + " Vector: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "lookAt":
                        node1.Nodes[0].Nodes.Add("BotAction: Turn Towards (Look at) Vector: " + act.getValueX() + ", " + act.getValueY() + ", " + act.getValueZ());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "stand":
                        node1.Nodes[0].Nodes.Add("BotAction: Stand");
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "attachTo":
                        node1.Nodes[0].Nodes.Add("BotAction: Attach object: " + act.getItemInv() + " Attach Point: " + act.getAttachPT());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "stopThread":
                        node1.Nodes[0].Nodes.Add("BotAction: Sleep Bot " + act.getTimeSleep() + " (milliseconds)");
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;

                    case "fly":
                        node1.Nodes[0].Nodes.Add("BotAction: Fly/Land ");
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;
                    
                    case "clickObject":
                        node1.Nodes[0].Nodes.Add("BotAction: Click Object: " + act.getLocalID());
                        node1.Nodes[0].Nodes[i++].ForeColor = System.Drawing.Color.Crimson;
                        break;
                }
            }
        }

        #endregion

    }
}
