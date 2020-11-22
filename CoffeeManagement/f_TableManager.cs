using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
                btn.Click += Btn_Click;
                btn.Tag = item;
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

        void showBill(int id)
        {
            ListViewBill.Items.Clear();
            //List<BillInfo> listBillInfo = BillInfoDAO.Instance.GetListBillInfo(BillDAO.Instance.GetUnCheckBillIDByTableID(id));
            List<CoffeeManagement.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            
            foreach (CoffeeManagement.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                ListViewBill.Items.Add(lsvItem);
            }
            tbTotalPrice.Text = totalPrice.ToString("c",new CultureInfo("vi-VN"));

        }

        #endregion

        #region event
        private void Btn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int tableID = ((sender as Button).Tag as Table).ID; 
            showBill(tableID);
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
        #endregion

        private void btnDiscount_Click(object sender, EventArgs e)
        {

        }

        private void f_TableManager_Load(object sender, EventArgs e)
        {

        }
    }
}
