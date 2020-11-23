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
using static CoffeeManagement.f_AccountProfile;

namespace CoffeeManagement
{
    public partial class f_TableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(LoginAccount.Type); }
        }
        public f_TableManager(Account acc)
        {

            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();
            Loadcategory();
            LoadComboboxTable(cbTable);
        }

        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            infoUserToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }

        void Loadcategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodFromCategory(int id)
        {
            List<Food> listFoods = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFoods;
            cbFood.DisplayMember = "Name";

        }
        public void LoadTable()
        {
            flpTable.Controls.Clear();
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
        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }

        #endregion

        #region event
        private void Btn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            int tableID = ((sender as Button).Tag as Table).ID;
            ListViewBill.Tag = (sender as Button).Tag;
            showBill(tableID);
        }
        private void infoUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_AccountProfile f = new f_AccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            infoUserToolStripMenuItem.Text = "Info account (" + e.Acc.DisplayName + ")";
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodFromCategory((cbCategory.SelectedItem as Category).ID);
            if (ListViewBill.Tag != null)
                showBill((ListViewBill.Tag as Table).ID);
        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodFromCategory((cbCategory.SelectedItem as Category).ID);
            if (ListViewBill.Tag != null)
                showBill((ListViewBill.Tag as Table).ID);
            LoadTable();
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodFromCategory((cbCategory.SelectedItem as Category).ID);
            if (ListViewBill.Tag != null)
                showBill((ListViewBill.Tag as Table).ID);
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_Admin f = new f_Admin();
            f.loginAccount = LoginAccount;
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }
        

        private void btnDiscount_Click(object sender, EventArgs e)
        {

        }

        private void f_TableManager_Load(object sender, EventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodFromCategory(id);
        }

        private void addFood_Click(object sender, EventArgs e)
        {
            Table table = ListViewBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("PSL Choose table");
                return;
            }

            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            showBill(table.ID);

            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = ListViewBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            int discount = (int)nmDiscount.Value;
            string[] a = tbTotalPrice.Text.Split(',');
            double totalPrice = Convert.ToDouble(a[0].Replace(".", ""));
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá\n=> {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {

                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                   // BillDAO.Instance.CheckOut(idBill);
                    showBill(table.ID);
                      
                    LoadTable();
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (ListViewBill.Tag as Table).ID;

            int id2 = (cbTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (ListViewBill.Tag as Table).Name, (cbTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();
            }
        }
        #endregion
    }
}
