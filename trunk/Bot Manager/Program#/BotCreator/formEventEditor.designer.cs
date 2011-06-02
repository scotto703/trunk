namespace BotGUI
{
    partial class formEventEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.txt_eventID = new System.Windows.Forms.TextBox();
            this.lbl_eventID = new System.Windows.Forms.Label();
            this.lbl_eventDescription = new System.Windows.Forms.Label();
            this.txt_eventDescription = new System.Windows.Forms.TextBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_chat = new System.Windows.Forms.TextBox();
            this.cbo_actionList = new System.Windows.Forms.ComboBox();
            this.lbl_actionList = new System.Windows.Forms.Label();
            this.lbl_chat = new System.Windows.Forms.Label();
            this.txt_region = new System.Windows.Forms.TextBox();
            this.lbl_region = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.num_valueZ = new System.Windows.Forms.NumericUpDown();
            this.num_valueY = new System.Windows.Forms.NumericUpDown();
            this.num_valueX = new System.Windows.Forms.NumericUpDown();
            this.btn_newAction = new System.Windows.Forms.Button();
            this.btn_deleteAction = new System.Windows.Forms.Button();
            this.cbo_attachPT = new System.Windows.Forms.ComboBox();
            this.lbl_miliseconds = new System.Windows.Forms.Label();
            this.lbl_attachPT = new System.Windows.Forms.Label();
            this.lbl_itemInv = new System.Windows.Forms.Label();
            this.txt_itemInv = new System.Windows.Forms.TextBox();
            this.lbl_UUID = new System.Windows.Forms.Label();
            this.txt_UUID = new System.Windows.Forms.TextBox();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.lbl_actionType = new System.Windows.Forms.Label();
            this.lbl_sleepTime = new System.Windows.Forms.Label();
            this.lblcoma2 = new System.Windows.Forms.Label();
            this.lblcoma1 = new System.Windows.Forms.Label();
            this.lbl_coordenates = new System.Windows.Forms.Label();
            this.txt_timer = new System.Windows.Forms.TextBox();
            this.txt_actionType = new System.Windows.Forms.TextBox();
            this.txt_sleepTime = new System.Windows.Forms.TextBox();
            this.btn_saveAction = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueX)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(321, 12);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(342, 439);
            this.treeView.TabIndex = 5;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // txt_eventID
            // 
            this.txt_eventID.Enabled = false;
            this.txt_eventID.Location = new System.Drawing.Point(78, 19);
            this.txt_eventID.Name = "txt_eventID";
            this.txt_eventID.Size = new System.Drawing.Size(50, 20);
            this.txt_eventID.TabIndex = 0;
            this.txt_eventID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_eventID
            // 
            this.lbl_eventID.AutoSize = true;
            this.lbl_eventID.Location = new System.Drawing.Point(20, 22);
            this.lbl_eventID.Name = "lbl_eventID";
            this.lbl_eventID.Size = new System.Drawing.Size(52, 13);
            this.lbl_eventID.TabIndex = 0;
            this.lbl_eventID.Text = "Event ID:";
            // 
            // lbl_eventDescription
            // 
            this.lbl_eventDescription.AutoSize = true;
            this.lbl_eventDescription.Location = new System.Drawing.Point(9, 46);
            this.lbl_eventDescription.Name = "lbl_eventDescription";
            this.lbl_eventDescription.Size = new System.Drawing.Size(63, 13);
            this.lbl_eventDescription.TabIndex = 2;
            this.lbl_eventDescription.Text = "Description:";
            // 
            // txt_eventDescription
            // 
            this.txt_eventDescription.Enabled = false;
            this.txt_eventDescription.Location = new System.Drawing.Point(78, 43);
            this.txt_eventDescription.Multiline = true;
            this.txt_eventDescription.Name = "txt_eventDescription";
            this.txt_eventDescription.Size = new System.Drawing.Size(217, 61);
            this.txt_eventDescription.TabIndex = 1;
            // 
            // btn_clear
            // 
            this.btn_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_clear.Location = new System.Drawing.Point(481, 457);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(88, 23);
            this.btn_clear.TabIndex = 0;
            this.btn_clear.Text = "Clea&r";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.Location = new System.Drawing.Point(575, 457);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(88, 23);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "&Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_eventDescription);
            this.groupBox1.Controls.Add(this.lbl_eventDescription);
            this.groupBox1.Controls.Add(this.lbl_eventID);
            this.groupBox1.Controls.Add(this.txt_eventID);
            this.groupBox1.Location = new System.Drawing.Point(7, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Information";
            // 
            // txt_chat
            // 
            this.txt_chat.Enabled = false;
            this.txt_chat.Location = new System.Drawing.Point(74, 46);
            this.txt_chat.Multiline = true;
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.Size = new System.Drawing.Size(217, 61);
            this.txt_chat.TabIndex = 1;
            // 
            // cbo_actionList
            // 
            this.cbo_actionList.Enabled = false;
            this.cbo_actionList.FormattingEnabled = true;
            this.cbo_actionList.Items.AddRange(new object[] {
            "animation",
            "attachTo",
            "chat",
            "lookAt",
            "sit",
            "stand",
            "stopThread",
            "teleport",
            "moveTo",
            "fly"});
            this.cbo_actionList.Location = new System.Drawing.Point(74, 19);
            this.cbo_actionList.Name = "cbo_actionList";
            this.cbo_actionList.Size = new System.Drawing.Size(217, 21);
            this.cbo_actionList.TabIndex = 0;
            this.cbo_actionList.SelectionChangeCommitted += new System.EventHandler(this.cbo_actionList_SelectionChangeCommitted);
            this.cbo_actionList.Leave += new System.EventHandler(this.cbo_actionList_Leave);
            // 
            // lbl_actionList
            // 
            this.lbl_actionList.AutoSize = true;
            this.lbl_actionList.Location = new System.Drawing.Point(1, 22);
            this.lbl_actionList.Name = "lbl_actionList";
            this.lbl_actionList.Size = new System.Drawing.Size(59, 13);
            this.lbl_actionList.TabIndex = 0;
            this.lbl_actionList.Text = "Action List:";
            // 
            // lbl_chat
            // 
            this.lbl_chat.AutoSize = true;
            this.lbl_chat.Location = new System.Drawing.Point(36, 49);
            this.lbl_chat.Name = "lbl_chat";
            this.lbl_chat.Size = new System.Drawing.Size(32, 13);
            this.lbl_chat.TabIndex = 2;
            this.lbl_chat.Text = "Chat:";
            // 
            // txt_region
            // 
            this.txt_region.Enabled = false;
            this.txt_region.Location = new System.Drawing.Point(74, 191);
            this.txt_region.Name = "txt_region";
            this.txt_region.Size = new System.Drawing.Size(217, 20);
            this.txt_region.TabIndex = 5;
            // 
            // lbl_region
            // 
            this.lbl_region.AutoSize = true;
            this.lbl_region.Location = new System.Drawing.Point(24, 194);
            this.lbl_region.Name = "lbl_region";
            this.lbl_region.Size = new System.Drawing.Size(44, 13);
            this.lbl_region.TabIndex = 10;
            this.lbl_region.Text = "Region:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.num_valueZ);
            this.groupBox2.Controls.Add(this.num_valueY);
            this.groupBox2.Controls.Add(this.num_valueX);
            this.groupBox2.Controls.Add(this.btn_newAction);
            this.groupBox2.Controls.Add(this.btn_deleteAction);
            this.groupBox2.Controls.Add(this.cbo_attachPT);
            this.groupBox2.Controls.Add(this.lbl_miliseconds);
            this.groupBox2.Controls.Add(this.lbl_attachPT);
            this.groupBox2.Controls.Add(this.lbl_itemInv);
            this.groupBox2.Controls.Add(this.txt_itemInv);
            this.groupBox2.Controls.Add(this.lbl_UUID);
            this.groupBox2.Controls.Add(this.txt_UUID);
            this.groupBox2.Controls.Add(this.lbl_timer);
            this.groupBox2.Controls.Add(this.lbl_actionType);
            this.groupBox2.Controls.Add(this.lbl_sleepTime);
            this.groupBox2.Controls.Add(this.lblcoma2);
            this.groupBox2.Controls.Add(this.lblcoma1);
            this.groupBox2.Controls.Add(this.lbl_coordenates);
            this.groupBox2.Controls.Add(this.txt_timer);
            this.groupBox2.Controls.Add(this.txt_actionType);
            this.groupBox2.Controls.Add(this.txt_sleepTime);
            this.groupBox2.Controls.Add(this.btn_saveAction);
            this.groupBox2.Controls.Add(this.lbl_region);
            this.groupBox2.Controls.Add(this.txt_region);
            this.groupBox2.Controls.Add(this.lbl_chat);
            this.groupBox2.Controls.Add(this.lbl_actionList);
            this.groupBox2.Controls.Add(this.cbo_actionList);
            this.groupBox2.Controls.Add(this.txt_chat);
            this.groupBox2.Location = new System.Drawing.Point(7, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 363);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Single Action";
            // 
            // num_valueZ
            // 
            this.num_valueZ.DecimalPlaces = 1;
            this.num_valueZ.Location = new System.Drawing.Point(235, 218);
            this.num_valueZ.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.num_valueZ.Name = "num_valueZ";
            this.num_valueZ.Size = new System.Drawing.Size(55, 20);
            this.num_valueZ.TabIndex = 8;
            // 
            // num_valueY
            // 
            this.num_valueY.DecimalPlaces = 1;
            this.num_valueY.Location = new System.Drawing.Point(154, 217);
            this.num_valueY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.num_valueY.Name = "num_valueY";
            this.num_valueY.Size = new System.Drawing.Size(61, 20);
            this.num_valueY.TabIndex = 7;
            // 
            // num_valueX
            // 
            this.num_valueX.DecimalPlaces = 1;
            this.num_valueX.Location = new System.Drawing.Point(74, 217);
            this.num_valueX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.num_valueX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.num_valueX.Name = "num_valueX";
            this.num_valueX.Size = new System.Drawing.Size(58, 20);
            this.num_valueX.TabIndex = 6;
            // 
            // btn_newAction
            // 
            this.btn_newAction.Location = new System.Drawing.Point(6, 327);
            this.btn_newAction.Name = "btn_newAction";
            this.btn_newAction.Size = new System.Drawing.Size(88, 23);
            this.btn_newAction.TabIndex = 13;
            this.btn_newAction.Text = "&New Action";
            this.btn_newAction.UseVisualStyleBackColor = true;
            this.btn_newAction.Click += new System.EventHandler(this.btn_newAction_Click);
            // 
            // btn_deleteAction
            // 
            this.btn_deleteAction.Location = new System.Drawing.Point(104, 327);
            this.btn_deleteAction.Name = "btn_deleteAction";
            this.btn_deleteAction.Size = new System.Drawing.Size(88, 23);
            this.btn_deleteAction.TabIndex = 14;
            this.btn_deleteAction.Text = "&Delete";
            this.btn_deleteAction.UseVisualStyleBackColor = true;
            this.btn_deleteAction.Click += new System.EventHandler(this.btn_deleteAction_Click);
            // 
            // cbo_attachPT
            // 
            this.cbo_attachPT.Enabled = false;
            this.cbo_attachPT.FormattingEnabled = true;
            this.cbo_attachPT.Items.AddRange(new object[] {
            "Chest",
            "Chin",
            "Default",
            "HUDBottom",
            "HUDBottomLeft",
            "HUDBottomRight",
            "HUDCenter",
            "HUDCenter2",
            "HUDTop",
            "HUDTopLeft",
            "HUDTopRight",
            "LeftEar",
            "LeftEyeball",
            "LeftFoot",
            "LeftForearm",
            "LeftHand",
            "LeftHip",
            "LeftLowerLeg",
            "LeftPec",
            "LeftShoulder",
            "LeftUpperArm",
            "LeftUpperLeg",
            "Mouth",
            "Nose",
            "Pelvis",
            "RightEar",
            "RightEyeball",
            "RightFoot",
            "RightForearm",
            "RightHand",
            "RightHip",
            "RightLowerLeg",
            "RightPec",
            "RightShoulder",
            "RightUpperArm",
            "RightUpperLeg",
            "Skull",
            "Spine",
            "Stomach"});
            this.cbo_attachPT.Location = new System.Drawing.Point(74, 165);
            this.cbo_attachPT.Name = "cbo_attachPT";
            this.cbo_attachPT.Size = new System.Drawing.Size(216, 21);
            this.cbo_attachPT.TabIndex = 4;
            // 
            // lbl_miliseconds
            // 
            this.lbl_miliseconds.AutoSize = true;
            this.lbl_miliseconds.Location = new System.Drawing.Point(134, 246);
            this.lbl_miliseconds.Name = "lbl_miliseconds";
            this.lbl_miliseconds.Size = new System.Drawing.Size(67, 13);
            this.lbl_miliseconds.TabIndex = 10;
            this.lbl_miliseconds.Text = "(miliseconds)";
            // 
            // lbl_attachPT
            // 
            this.lbl_attachPT.AutoSize = true;
            this.lbl_attachPT.Location = new System.Drawing.Point(10, 168);
            this.lbl_attachPT.Name = "lbl_attachPT";
            this.lbl_attachPT.Size = new System.Drawing.Size(58, 13);
            this.lbl_attachPT.TabIndex = 8;
            this.lbl_attachPT.Text = "Attach PT:";
            // 
            // lbl_itemInv
            // 
            this.lbl_itemInv.AutoSize = true;
            this.lbl_itemInv.Location = new System.Drawing.Point(20, 142);
            this.lbl_itemInv.Name = "lbl_itemInv";
            this.lbl_itemInv.Size = new System.Drawing.Size(48, 13);
            this.lbl_itemInv.TabIndex = 6;
            this.lbl_itemInv.Text = "Item Inv:";
            // 
            // txt_itemInv
            // 
            this.txt_itemInv.Enabled = false;
            this.txt_itemInv.Location = new System.Drawing.Point(74, 139);
            this.txt_itemInv.Name = "txt_itemInv";
            this.txt_itemInv.Size = new System.Drawing.Size(216, 20);
            this.txt_itemInv.TabIndex = 3;
            // 
            // lbl_UUID
            // 
            this.lbl_UUID.AutoSize = true;
            this.lbl_UUID.Location = new System.Drawing.Point(31, 116);
            this.lbl_UUID.Name = "lbl_UUID";
            this.lbl_UUID.Size = new System.Drawing.Size(37, 13);
            this.lbl_UUID.TabIndex = 4;
            this.lbl_UUID.Text = "UUID:";
            // 
            // txt_UUID
            // 
            this.txt_UUID.Enabled = false;
            this.txt_UUID.Location = new System.Drawing.Point(74, 113);
            this.txt_UUID.Name = "txt_UUID";
            this.txt_UUID.Size = new System.Drawing.Size(216, 20);
            this.txt_UUID.TabIndex = 2;
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(32, 298);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(36, 13);
            this.lbl_timer.TabIndex = 23;
            this.lbl_timer.Text = "Timer:";
            // 
            // lbl_actionType
            // 
            this.lbl_actionType.AutoSize = true;
            this.lbl_actionType.Location = new System.Drawing.Point(5, 272);
            this.lbl_actionType.Name = "lbl_actionType";
            this.lbl_actionType.Size = new System.Drawing.Size(63, 13);
            this.lbl_actionType.TabIndex = 21;
            this.lbl_actionType.Text = "Action type:";
            // 
            // lbl_sleepTime
            // 
            this.lbl_sleepTime.AutoSize = true;
            this.lbl_sleepTime.Location = new System.Drawing.Point(5, 246);
            this.lbl_sleepTime.Name = "lbl_sleepTime";
            this.lbl_sleepTime.Size = new System.Drawing.Size(63, 13);
            this.lbl_sleepTime.TabIndex = 18;
            this.lbl_sleepTime.Text = "Sleep Time:";
            // 
            // lblcoma2
            // 
            this.lblcoma2.AutoSize = true;
            this.lblcoma2.Location = new System.Drawing.Point(218, 220);
            this.lblcoma2.Name = "lblcoma2";
            this.lblcoma2.Size = new System.Drawing.Size(10, 13);
            this.lblcoma2.TabIndex = 16;
            this.lblcoma2.Text = ",";
            // 
            // lblcoma1
            // 
            this.lblcoma1.AutoSize = true;
            this.lblcoma1.Location = new System.Drawing.Point(138, 220);
            this.lblcoma1.Name = "lblcoma1";
            this.lblcoma1.Size = new System.Drawing.Size(10, 13);
            this.lblcoma1.TabIndex = 14;
            this.lblcoma1.Text = ",";
            // 
            // lbl_coordenates
            // 
            this.lbl_coordenates.AutoSize = true;
            this.lbl_coordenates.Location = new System.Drawing.Point(1, 220);
            this.lbl_coordenates.Name = "lbl_coordenates";
            this.lbl_coordenates.Size = new System.Drawing.Size(67, 13);
            this.lbl_coordenates.TabIndex = 12;
            this.lbl_coordenates.Text = "Value (x,y,z):";
            // 
            // txt_timer
            // 
            this.txt_timer.Enabled = false;
            this.txt_timer.Location = new System.Drawing.Point(74, 295);
            this.txt_timer.Name = "txt_timer";
            this.txt_timer.Size = new System.Drawing.Size(56, 20);
            this.txt_timer.TabIndex = 12;
            // 
            // txt_actionType
            // 
            this.txt_actionType.Enabled = false;
            this.txt_actionType.Location = new System.Drawing.Point(74, 269);
            this.txt_actionType.Name = "txt_actionType";
            this.txt_actionType.Size = new System.Drawing.Size(56, 20);
            this.txt_actionType.TabIndex = 11;
            // 
            // txt_sleepTime
            // 
            this.txt_sleepTime.Enabled = false;
            this.txt_sleepTime.Location = new System.Drawing.Point(74, 243);
            this.txt_sleepTime.Name = "txt_sleepTime";
            this.txt_sleepTime.Size = new System.Drawing.Size(56, 20);
            this.txt_sleepTime.TabIndex = 9;
            // 
            // btn_saveAction
            // 
            this.btn_saveAction.Location = new System.Drawing.Point(202, 327);
            this.btn_saveAction.Name = "btn_saveAction";
            this.btn_saveAction.Size = new System.Drawing.Size(88, 23);
            this.btn_saveAction.TabIndex = 15;
            this.btn_saveAction.Text = "S&ave Action";
            this.btn_saveAction.UseVisualStyleBackColor = true;
            this.btn_saveAction.Click += new System.EventHandler(this.btn_saveAction_Click);
            // 
            // formEventEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 499);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.treeView);
            this.MinimumSize = new System.Drawing.Size(683, 522);
            this.Name = "formEventEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Event Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_valueX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox txt_eventID;
        private System.Windows.Forms.Label lbl_eventID;
        private System.Windows.Forms.Label lbl_eventDescription;
        private System.Windows.Forms.TextBox txt_eventDescription;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_chat;
        private System.Windows.Forms.ComboBox cbo_actionList;
        private System.Windows.Forms.Label lbl_actionList;
        private System.Windows.Forms.Label lbl_chat;
        private System.Windows.Forms.TextBox txt_region;
        private System.Windows.Forms.Label lbl_region;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_saveAction;
        private System.Windows.Forms.TextBox txt_timer;
        private System.Windows.Forms.TextBox txt_actionType;
        private System.Windows.Forms.TextBox txt_sleepTime;
        private System.Windows.Forms.Label lblcoma2;
        private System.Windows.Forms.Label lblcoma1;
        private System.Windows.Forms.Label lbl_coordenates;
        private System.Windows.Forms.Label lbl_actionType;
        private System.Windows.Forms.Label lbl_sleepTime;
        private System.Windows.Forms.Label lbl_timer;
        private System.Windows.Forms.Label lbl_UUID;
        private System.Windows.Forms.TextBox txt_UUID;
        private System.Windows.Forms.TextBox txt_itemInv;
        private System.Windows.Forms.Label lbl_itemInv;
        private System.Windows.Forms.Label lbl_attachPT;
        private System.Windows.Forms.Label lbl_miliseconds;
        private System.Windows.Forms.ComboBox cbo_attachPT;
        private System.Windows.Forms.Button btn_newAction;
        private System.Windows.Forms.Button btn_deleteAction;
        private System.Windows.Forms.NumericUpDown num_valueX;
        private System.Windows.Forms.NumericUpDown num_valueZ;
        private System.Windows.Forms.NumericUpDown num_valueY;

    }
}