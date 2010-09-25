namespace BotGUI
{
    partial class frmClassMngr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClassMngr));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botManagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationManagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howDoIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.botGB = new System.Windows.Forms.GroupBox();
            this.botLB = new System.Windows.Forms.ListBox();
            this.simGB = new System.Windows.Forms.GroupBox();
            this.simLB = new System.Windows.Forms.ListBox();
            this.tabGB = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.changeButton = new System.Windows.Forms.Button();
            this.consoleRTB = new System.Windows.Forms.RichTextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.newBotB = new System.Windows.Forms.ToolStripButton();
            this.loadBotB = new System.Windows.Forms.ToolStripButton();
            this.editBotB = new System.Windows.Forms.ToolStripButton();
            this.botStopB = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.newSimB = new System.Windows.Forms.ToolStripButton();
            this.loadSimB = new System.Windows.Forms.ToolStripButton();
            this.editSimB = new System.Windows.Forms.ToolStripButton();
            this.simStopB = new System.Windows.Forms.ToolStripButton();
            this.consoleB = new System.Windows.Forms.ToolStripButton();
            this.ListBotGB = new System.Windows.Forms.GroupBox();
            this.listAvailBot = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.botGB.SuspendLayout();
            this.simGB.SuspendLayout();
            this.tabGB.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ListBotGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(706, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSimulationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadSimulationToolStripMenuItem
            // 
            this.loadSimulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulationToolStripMenuItem,
            this.botToolStripMenuItem});
            this.loadSimulationToolStripMenuItem.Name = "loadSimulationToolStripMenuItem";
            this.loadSimulationToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadSimulationToolStripMenuItem.Text = "&Load";
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.simulationToolStripMenuItem.Text = "&Simulation";
            // 
            // botToolStripMenuItem
            // 
            this.botToolStripMenuItem.Name = "botToolStripMenuItem";
            this.botToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.botToolStripMenuItem.Text = "&Bot";
            this.botToolStripMenuItem.Click += new System.EventHandler(this.botToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.botManagerToolStripMenuItem1,
            this.simulationManagerToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // botManagerToolStripMenuItem1
            // 
            this.botManagerToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("botManagerToolStripMenuItem1.Image")));
            this.botManagerToolStripMenuItem1.Name = "botManagerToolStripMenuItem1";
            this.botManagerToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.botManagerToolStripMenuItem1.Text = "&Bot Manager";
            this.botManagerToolStripMenuItem1.Click += new System.EventHandler(this.botManagerToolStripMenuItem1_Click);
            // 
            // simulationManagerToolStripMenuItem1
            // 
            this.simulationManagerToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("simulationManagerToolStripMenuItem1.Image")));
            this.simulationManagerToolStripMenuItem1.Name = "simulationManagerToolStripMenuItem1";
            this.simulationManagerToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.simulationManagerToolStripMenuItem1.Text = "&Simulation Manager";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howDoIToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // howDoIToolStripMenuItem
            // 
            this.howDoIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("howDoIToolStripMenuItem.Image")));
            this.howDoIToolStripMenuItem.Name = "howDoIToolStripMenuItem";
            this.howDoIToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.howDoIToolStripMenuItem.Text = "&How Do I";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel1.Text = "Running Bots: ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(123, 17);
            this.toolStripStatusLabel2.Text = "Running Simulations: ";
            // 
            // botGB
            // 
            this.botGB.Controls.Add(this.botLB);
            this.botGB.Location = new System.Drawing.Point(238, 54);
            this.botGB.Name = "botGB";
            this.botGB.Size = new System.Drawing.Size(220, 120);
            this.botGB.TabIndex = 2;
            this.botGB.TabStop = false;
            this.botGB.Text = "Active Bot";
            // 
            // botLB
            // 
            this.botLB.FormattingEnabled = true;
            this.botLB.Location = new System.Drawing.Point(6, 15);
            this.botLB.Name = "botLB";
            this.botLB.Size = new System.Drawing.Size(200, 95);
            this.botLB.Sorted = true;
            this.botLB.TabIndex = 0;
            // 
            // simGB
            // 
            this.simGB.Controls.Add(this.simLB);
            this.simGB.Location = new System.Drawing.Point(464, 54);
            this.simGB.Name = "simGB";
            this.simGB.Size = new System.Drawing.Size(220, 120);
            this.simGB.TabIndex = 3;
            this.simGB.TabStop = false;
            this.simGB.Text = "Active Simulations";
            // 
            // simLB
            // 
            this.simLB.FormattingEnabled = true;
            this.simLB.Location = new System.Drawing.Point(6, 15);
            this.simLB.Name = "simLB";
            this.simLB.Size = new System.Drawing.Size(200, 95);
            this.simLB.Sorted = true;
            this.simLB.TabIndex = 0;
            // 
            // tabGB
            // 
            this.tabGB.Controls.Add(this.tabControl1);
            this.tabGB.Location = new System.Drawing.Point(12, 180);
            this.tabGB.Name = "tabGB";
            this.tabGB.Size = new System.Drawing.Size(672, 212);
            this.tabGB.TabIndex = 4;
            this.tabGB.TabStop = false;
            this.tabGB.Text = "Bot Status";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(660, 187);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.changeButton);
            this.tabPage1.Controls.Add(this.consoleRTB);
            this.tabPage1.Controls.Add(this.stopButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(652, 161);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(546, 6);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(100, 76);
            this.changeButton.TabIndex = 2;
            this.changeButton.Text = "Change Path";
            this.changeButton.UseVisualStyleBackColor = true;
            // 
            // consoleRTB
            // 
            this.consoleRTB.BackColor = System.Drawing.SystemColors.Window;
            this.consoleRTB.ForeColor = System.Drawing.SystemColors.WindowText;
            this.consoleRTB.Location = new System.Drawing.Point(3, 6);
            this.consoleRTB.Name = "consoleRTB";
            this.consoleRTB.ReadOnly = true;
            this.consoleRTB.Size = new System.Drawing.Size(537, 149);
            this.consoleRTB.TabIndex = 1;
            this.consoleRTB.Text = "";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(546, 88);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(100, 70);
            this.stopButton.TabIndex = 0;
            this.stopButton.Text = "Stop Bot";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.newBotB,
            this.loadBotB,
            this.editBotB,
            this.botStopB,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.newSimB,
            this.loadSimB,
            this.editSimB,
            this.simStopB,
            this.consoleB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(706, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel1.Text = "Bot";
            // 
            // newBotB
            // 
            this.newBotB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newBotB.Image = ((System.Drawing.Image)(resources.GetObject("newBotB.Image")));
            this.newBotB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newBotB.Name = "newBotB";
            this.newBotB.Size = new System.Drawing.Size(23, 22);
            this.newBotB.Text = "New Bot";
            this.newBotB.Click += new System.EventHandler(this.newBotB_Click);
            // 
            // loadBotB
            // 
            this.loadBotB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadBotB.Image = ((System.Drawing.Image)(resources.GetObject("loadBotB.Image")));
            this.loadBotB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadBotB.Name = "loadBotB";
            this.loadBotB.Size = new System.Drawing.Size(23, 22);
            this.loadBotB.Text = "Load Bot";
            this.loadBotB.Click += new System.EventHandler(this.loadBotB_Click);
            // 
            // editBotB
            // 
            this.editBotB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editBotB.Image = ((System.Drawing.Image)(resources.GetObject("editBotB.Image")));
            this.editBotB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editBotB.Name = "editBotB";
            this.editBotB.Size = new System.Drawing.Size(23, 22);
            this.editBotB.Text = "Edit Bot";
            this.editBotB.Click += new System.EventHandler(this.editBotB_Click);
            // 
            // botStopB
            // 
            this.botStopB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.botStopB.Image = ((System.Drawing.Image)(resources.GetObject("botStopB.Image")));
            this.botStopB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.botStopB.Name = "botStopB";
            this.botStopB.Size = new System.Drawing.Size(23, 22);
            this.botStopB.Text = "Stop Bot";
            this.botStopB.Click += new System.EventHandler(this.botStopB_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Bot Events";
            this.toolStripButton1.Click += new System.EventHandler(this.EventViewerToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel2.Text = "Sim";
            // 
            // newSimB
            // 
            this.newSimB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newSimB.Image = ((System.Drawing.Image)(resources.GetObject("newSimB.Image")));
            this.newSimB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newSimB.Name = "newSimB";
            this.newSimB.Size = new System.Drawing.Size(23, 22);
            this.newSimB.Text = "New Simulation";
            // 
            // loadSimB
            // 
            this.loadSimB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadSimB.Image = ((System.Drawing.Image)(resources.GetObject("loadSimB.Image")));
            this.loadSimB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadSimB.Name = "loadSimB";
            this.loadSimB.Size = new System.Drawing.Size(23, 22);
            this.loadSimB.Text = "Load Simulation";
            // 
            // editSimB
            // 
            this.editSimB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editSimB.Enabled = false;
            this.editSimB.Image = ((System.Drawing.Image)(resources.GetObject("editSimB.Image")));
            this.editSimB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editSimB.Name = "editSimB";
            this.editSimB.Size = new System.Drawing.Size(23, 22);
            this.editSimB.Text = "Edit Simulation";
            // 
            // simStopB
            // 
            this.simStopB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.simStopB.Enabled = false;
            this.simStopB.Image = ((System.Drawing.Image)(resources.GetObject("simStopB.Image")));
            this.simStopB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.simStopB.Name = "simStopB";
            this.simStopB.Size = new System.Drawing.Size(23, 22);
            this.simStopB.Text = "Stop Simulation";
            // 
            // consoleB
            // 
            this.consoleB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.consoleB.Enabled = false;
            this.consoleB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.consoleB.Name = "consoleB";
            this.consoleB.Size = new System.Drawing.Size(23, 22);
            this.consoleB.Text = "Toggle Console";
            // 
            // ListBotGB
            // 
            this.ListBotGB.Controls.Add(this.listAvailBot);
            this.ListBotGB.Location = new System.Drawing.Point(12, 54);
            this.ListBotGB.Name = "ListBotGB";
            this.ListBotGB.Size = new System.Drawing.Size(220, 120);
            this.ListBotGB.TabIndex = 6;
            this.ListBotGB.TabStop = false;
            this.ListBotGB.Text = "Available Bot List";
            // 
            // listAvailBot
            // 
            this.listAvailBot.FormattingEnabled = true;
            this.listAvailBot.Location = new System.Drawing.Point(6, 16);
            this.listAvailBot.Name = "listAvailBot";
            this.listAvailBot.Size = new System.Drawing.Size(200, 95);
            this.listAvailBot.TabIndex = 0;
            // 
            // frmClassMngr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 436);
            this.Controls.Add(this.ListBotGB);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabGB);
            this.Controls.Add(this.simGB);
            this.Controls.Add(this.botGB);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmClassMngr";
            this.Text = "Bot Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.botGB.ResumeLayout(false);
            this.simGB.ResumeLayout(false);
            this.tabGB.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ListBotGB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem botToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howDoIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox botGB;
        private System.Windows.Forms.GroupBox simGB;
        private System.Windows.Forms.GroupBox tabGB;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem botManagerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem simulationManagerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ListBox botLB;
        private System.Windows.Forms.ListBox simLB;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton botStopB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton simStopB;
        private System.Windows.Forms.ToolStripButton consoleB;
        private System.Windows.Forms.ToolStripButton newBotB;
        private System.Windows.Forms.ToolStripButton loadBotB;
        private System.Windows.Forms.ToolStripButton newSimB;
        private System.Windows.Forms.ToolStripButton editBotB;
        private System.Windows.Forms.ToolStripButton loadSimB;
        private System.Windows.Forms.ToolStripButton editSimB;
        private System.Windows.Forms.GroupBox ListBotGB;
        private System.Windows.Forms.ListBox listAvailBot;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.RichTextBox consoleRTB;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

