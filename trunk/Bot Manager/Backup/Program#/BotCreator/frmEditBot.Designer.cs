namespace BotGUI
{
    partial class frmEditBot
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
            this.components = new System.ComponentModel.Container();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblCoordinates = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtCoordinates = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnSettingsPath = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboBotList = new System.Windows.Forms.ComboBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dlgAIMLPath = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSettingsPath = new System.Windows.Forms.Label();
            this.btnAIMLpath = new System.Windows.Forms.Button();
            this.dlgSettingsPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(0, 264);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(101, 23);
            this.lblFilePath.TabIndex = 25;
            this.lblFilePath.Text = "ADD AIML ";
            // 
            // lblCoordinates
            // 
            this.lblCoordinates.AutoSize = true;
            this.lblCoordinates.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoordinates.Location = new System.Drawing.Point(8, 224);
            this.lblCoordinates.Name = "lblCoordinates";
            this.lblCoordinates.Size = new System.Drawing.Size(108, 23);
            this.lblCoordinates.TabIndex = 24;
            this.lblCoordinates.Text = "Coordinates";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(37, 184);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(79, 23);
            this.lblLocation.TabIndex = 23;
            this.lblLocation.Text = "Location";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(28, 144);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 23);
            this.lblPassword.TabIndex = 22;
            this.lblPassword.Text = "Password";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(18, 104);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(98, 23);
            this.lblLastName.TabIndex = 21;
            this.lblLastName.Text = "Last Name";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(16, 64);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(100, 23);
            this.lblFirstName.TabIndex = 20;
            this.lblFirstName.Text = "First Name";
            // 
            // txtCoordinates
            // 
            this.txtCoordinates.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoordinates.Location = new System.Drawing.Point(122, 224);
            this.txtCoordinates.Name = "txtCoordinates";
            this.txtCoordinates.Size = new System.Drawing.Size(158, 27);
            this.txtCoordinates.TabIndex = 25;
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(122, 184);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(158, 27);
            this.txtLocation.TabIndex = 20;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(122, 144);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(158, 27);
            this.txtPassword.TabIndex = 15;
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(122, 104);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(158, 27);
            this.txtLastName.TabIndex = 10;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(122, 64);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(158, 27);
            this.txtFirstName.TabIndex = 5;
            // 
            // btnSettingsPath
            // 
            this.btnSettingsPath.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSettingsPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSettingsPath.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingsPath.Location = new System.Drawing.Point(160, 296);
            this.btnSettingsPath.Name = "btnSettingsPath";
            this.btnSettingsPath.Size = new System.Drawing.Size(120, 29);
            this.btnSettingsPath.TabIndex = 35;
            this.btnSettingsPath.Text = "Change Path";
            this.btnSettingsPath.UseVisualStyleBackColor = false;
            this.btnSettingsPath.Click += new System.EventHandler(this.btnSettingsPath_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(200, 424);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 40);
            this.btnExit.TabIndex = 55;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(0, 344);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 40);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUndo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.Location = new System.Drawing.Point(101, 344);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(91, 40);
            this.btnUndo.TabIndex = 45;
            this.btnUndo.Text = "&Undo";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(200, 344);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 40);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.Text = "&Delete Bot";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboBotList
            // 
            this.cboBotList.FormattingEnabled = true;
            this.cboBotList.Location = new System.Drawing.Point(16, 16);
            this.cboBotList.Name = "cboBotList";
            this.cboBotList.Size = new System.Drawing.Size(264, 27);
            this.cboBotList.TabIndex = 1;
            this.cboBotList.SelectedIndexChanged += new System.EventHandler(this.cboBotList_SelectedIndexChanged);
            // 
            // dlgAIMLPath
            // 
            this.dlgAIMLPath.Description = "Select the folder that contains the AIML files for your bot";
            this.dlgAIMLPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.dlgAIMLPath.ShowNewFolderButton = false;
            // 
            // lblSettingsPath
            // 
            this.lblSettingsPath.AutoSize = true;
            this.lblSettingsPath.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsPath.Location = new System.Drawing.Point(0, 298);
            this.lblSettingsPath.Name = "lblSettingsPath";
            this.lblSettingsPath.Size = new System.Drawing.Size(154, 23);
            this.lblSettingsPath.TabIndex = 33;
            this.lblSettingsPath.Text = "Settings File Path";
            // 
            // btnAIMLpath
            // 
            this.btnAIMLpath.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnAIMLpath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAIMLpath.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAIMLpath.Location = new System.Drawing.Point(160, 262);
            this.btnAIMLpath.Name = "btnAIMLpath";
            this.btnAIMLpath.Size = new System.Drawing.Size(120, 29);
            this.btnAIMLpath.TabIndex = 30;
            this.btnAIMLpath.Text = "Select Folder";
            this.btnAIMLpath.UseVisualStyleBackColor = false;
            this.btnAIMLpath.Click += new System.EventHandler(this.btnAIMLpath_Click);
            // 
            // dlgSettingsPath
            // 
            this.dlgSettingsPath.DefaultExt = "xml";
            this.dlgSettingsPath.Filter = "XML documents (.xml)|*.xml";
            this.dlgSettingsPath.Title = "Select Bot Settings XML";
            // 
            // frmEditBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(292, 470);
            this.ControlBox = false;
            this.Controls.Add(this.btnAIMLpath);
            this.Controls.Add(this.lblSettingsPath);
            this.Controls.Add(this.cboBotList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnSettingsPath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.lblCoordinates);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtCoordinates);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEditBot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblCoordinates;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtCoordinates;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnSettingsPath;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboBotList;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.FolderBrowserDialog dlgAIMLPath;
        private System.Windows.Forms.Label lblSettingsPath;
        private System.Windows.Forms.Button btnAIMLpath;
        private System.Windows.Forms.OpenFileDialog dlgSettingsPath;
    }
}