using CoffeeManagement.DAO;
using QuanLiQuanCafe.DAO;
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
            LoadTable();
        }

        #region Method
         public void LoadTable()
        {
            List<Table> tablelist = TableDAO.Instance.LoadTableList();
            foreach(Table item in tablelist)
            {

                Button btn = new Button() {Width= TableDAO.tableHeight, Height= TableDAO.tableWidth };

                btn.Text = item.Name + Environment.NewLine + item.Status;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }
        #endregion

        #region event
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
        #endregion
    }
}
