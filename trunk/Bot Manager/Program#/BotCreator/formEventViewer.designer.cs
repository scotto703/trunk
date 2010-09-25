namespace BotGUI
{
    partial class formEventViewer
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eventListLB = new System.Windows.Forms.ListBox();
            this.EventLbl = new System.Windows.Forms.Label();
            this.QuestionLbl = new System.Windows.Forms.Label();
            this.questionsLB = new System.Windows.Forms.ListBox();
            this.addEventBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.removeBtn = new System.Windows.Forms.Button();
            this.Exit_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 159);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(544, 304);
            this.treeView1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(229, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Bot :";
            // 
            // eventListLB
            // 
            this.eventListLB.FormattingEnabled = true;
            this.eventListLB.Location = new System.Drawing.Point(259, 49);
            this.eventListLB.Name = "eventListLB";
            this.eventListLB.Size = new System.Drawing.Size(297, 95);
            this.eventListLB.TabIndex = 5;
            this.eventListLB.SelectedIndexChanged += new System.EventHandler(this.eventListLB_SelectedIndexChanged);
            // 
            // EventLbl
            // 
            this.EventLbl.AutoSize = true;
            this.EventLbl.Location = new System.Drawing.Point(259, 33);
            this.EventLbl.Name = "EventLbl";
            this.EventLbl.Size = new System.Drawing.Size(74, 13);
            this.EventLbl.TabIndex = 6;
            this.EventLbl.Text = "Select Event :";
            this.EventLbl.Visible = false;
            // 
            // QuestionLbl
            // 
            this.QuestionLbl.AutoSize = true;
            this.QuestionLbl.Location = new System.Drawing.Point(12, 492);
            this.QuestionLbl.Name = "QuestionLbl";
            this.QuestionLbl.Size = new System.Drawing.Size(130, 13);
            this.QuestionLbl.TabIndex = 8;
            this.QuestionLbl.Text = "AIML Trigger Question(s) :";
            // 
            // questionsLB
            // 
            this.questionsLB.FormattingEnabled = true;
            this.questionsLB.Location = new System.Drawing.Point(15, 509);
            this.questionsLB.Name = "questionsLB";
            this.questionsLB.Size = new System.Drawing.Size(541, 69);
            this.questionsLB.TabIndex = 9;
            // 
            // addEventBtn
            // 
            this.addEventBtn.Location = new System.Drawing.Point(12, 76);
            this.addEventBtn.Name = "addEventBtn";
            this.addEventBtn.Size = new System.Drawing.Size(98, 23);
            this.addEventBtn.TabIndex = 10;
            this.addEventBtn.Text = "Add Event";
            this.addEventBtn.UseVisualStyleBackColor = true;
            this.addEventBtn.Click += new System.EventHandler(this.addEventBtn_Click);
            // 
            // editBtn
            // 
            this.editBtn.Enabled = false;
            this.editBtn.Location = new System.Drawing.Point(12, 105);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(98, 23);
            this.editBtn.TabIndex = 11;
            this.editBtn.Text = "Edit Event";
            this.editBtn.UseVisualStyleBackColor = true;
            // 
            // removeBtn
            // 
            this.removeBtn.Enabled = false;
            this.removeBtn.Location = new System.Drawing.Point(12, 134);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(98, 23);
            this.removeBtn.TabIndex = 12;
            this.removeBtn.Text = "Remove Event";
            this.removeBtn.UseVisualStyleBackColor = true;
            // 
            // Exit_button
            // 
            this.Exit_button.Location = new System.Drawing.Point(480, 595);
            this.Exit_button.Name = "Exit_button";
            this.Exit_button.Size = new System.Drawing.Size(75, 23);
            this.Exit_button.TabIndex = 13;
            this.Exit_button.Text = "Exit";
            this.Exit_button.UseVisualStyleBackColor = true;
            this.Exit_button.Click += new System.EventHandler(this.Exit_button_Click);
            // 
            // formEventViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 630);
            this.Controls.Add(this.Exit_button);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.addEventBtn);
            this.Controls.Add(this.questionsLB);
            this.Controls.Add(this.QuestionLbl);
            this.Controls.Add(this.EventLbl);
            this.Controls.Add(this.eventListLB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.treeView1);
            this.Name = "formEventViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox eventListLB;
        private System.Windows.Forms.Label EventLbl;
        private System.Windows.Forms.Label QuestionLbl;
        private System.Windows.Forms.ListBox questionsLB;
        private System.Windows.Forms.Button addEventBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.Button Exit_button;
    }
}

