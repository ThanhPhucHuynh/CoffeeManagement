using System;
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
    public partial class f_TableManager : Form
    {
        public f_TableManager()
        {
            InitializeComponent();
        }

        private void infoUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_AccountProfile f = new f_AccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_Admin f = new f_Admin();
            f.ShowDialog();
        }
    }
}
