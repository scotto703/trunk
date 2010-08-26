//******************************************
//***Written by Allan Blackford
//***
//***Las Modified: 7/21/2008
//******************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BotGUI
{
    public partial class frmChoice : Form
    {
        public frmChoice()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //open create window and hide choice window
            frmCreate NewBot = new frmCreate();
            NewBot.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //open edit window and choice window
            frmEditBot EditBot = new frmEditBot();
            EditBot.Show();
            this.Hide();
        }
    }
}