namespace BotGUI
{
    partial class frmNewEvent
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
            this.lblEventName = new System.Windows.Forms.Label();
            this.tb_EventName = new System.Windows.Forms.TextBox();
            this.lblAimlQuestion = new System.Windows.Forms.Label();
            this.tb_AimlQuestion = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btn_Create = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEventName
            // 
            this.lblEventName.AutoSize = true;
            this.lblEventName.Location = new System.Drawing.Point(13, 52);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(163, 13);
            this.lblEventName.TabIndex = 0;
            this.lblEventName.Text = "Enter a name for the new Event: ";
            // 
            // tb_EventName
            // 
            this.tb_EventName.Location = new System.Drawing.Point(16, 77);
            this.tb_EventName.Name = "tb_EventName";
            this.tb_EventName.Size = new System.Drawing.Size(365, 20);
            this.tb_EventName.TabIndex = 1;
            // 
            // lblAimlQuestion
            // 
            this.lblAimlQuestion.AutoSize = true;
            this.lblAimlQuestion.Location = new System.Drawing.Point(13, 129);
            this.lblAimlQuestion.Name = "lblAimlQuestion";
            this.lblAimlQuestion.Size = new System.Drawing.Size(180, 13);
            this.lblAimlQuestion.TabIndex = 2;
            this.lblAimlQuestion.Text = "Enter AIML Question for new Event: ";
            // 
            // tb_AimlQuestion
            // 
            this.tb_AimlQuestion.Location = new System.Drawing.Point(16, 154);
            this.tb_AimlQuestion.Name = "tb_AimlQuestion";
            this.tb_AimlQuestion.Size = new System.Drawing.Size(365, 20);
            this.tb_AimlQuestion.TabIndex = 3;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(16, 13);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(153, 20);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = "Create New Event";
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(275, 238);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_Create.TabIndex = 5;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_CreateEvent_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(356, 238);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // frmNewEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 273);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.tb_AimlQuestion);
            this.Controls.Add(this.lblAimlQuestion);
            this.Controls.Add(this.tb_EventName);
            this.Controls.Add(this.lblEventName);
            this.Name = "frmNewEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Event";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.TextBox tb_EventName;
        private System.Windows.Forms.Label lblAimlQuestion;
        private System.Windows.Forms.TextBox tb_AimlQuestion;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Button btn_Cancel;
    }
}