﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class f_Login : Form
    {
        public f_Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void f_LoginClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do you want exit Coffee Managemenrt?","Notification", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            f_TableManager f = new f_TableManager();
            this.Hide();
            f.ShowDialog(); // this Dialog is top mode 
            this.Show();
        }
    }
}
