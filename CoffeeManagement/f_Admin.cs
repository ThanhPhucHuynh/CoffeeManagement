
using CoffeeManagement.DAO;
using CoffeeManagement.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class f_Admin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account loginAccount;
        public f_Admin()
        {

            InitializeComponent();  

           
            LoadData();
        }

        #region methods
        void LoadData()
        {
            dtgwFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;

           // LoadAccountList();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadAccount();
            LoadListFood();
            LoadTable();
            LoadCategory();
            LoadCategoryIntoCombobox(cbCategoryFood);
            AddFoodBinding();
            AddTableBinding();
            addCategoryBinding();
            AddAccountBinding();
            
        }
        void LoadTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void LoadCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        { 
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }
        void AddCatagory(string name)
        {
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm Category thành công");
            }
            else
            {
                MessageBox.Show("Thêm Category thất bại");
            }

            LoadCategory();
        }
        void AddTable(string name)
        {
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm Table thành công");
            }
            else
            {
                MessageBox.Show("Thêm Table thất bại");
            }

            LoadTable();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }



        #endregion

        #region event


        private void label9_Click(object sender, EventArgs e)
        {

        }
        void LoadAccountList()
        {

            string query = "EXEC USP_GetAccountByUserName @userName";
            
            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query,new object[] {"staff" });

        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddFoodBinding()
        {
            tbNameFood.DataBindings.Add(new Binding("Text", dtgwFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            tbIdFood.DataBindings.Add(new Binding("Text", dtgwFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmPriceFood.DataBindings.Add(new Binding("Value", dtgwFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            tbIdTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            tbNameTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            tbStatusTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));

        }
        void AddAccountBinding()
        {
            tbAccountAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            tbNameAccount.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            cbAccountType.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void addCategoryBinding()
        {
            tbIDCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            tbNameCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));

        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }




        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        #endregion

        private void btnViewFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void tbIdFood_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgwFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgwFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbCategoryFood.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCategoryFood.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbCategoryFood.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = tbNameFood.Text;
            int categoryID = (cbCategoryFood.SelectedItem as Category).ID;
            float price = (float)nmPriceFood.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = tbNameFood.Text;
            int categoryID = (cbCategoryFood.SelectedItem as Category).ID;
            float price = (float)nmPriceFood.Value;
            int id = Convert.ToInt32(tbIdFood.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDelFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbIdFood.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }
        #region event


        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        #endregion

        private void btnFindFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(tbFindFood.Text);
        }

        private void tbFindFood_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = tbAccountAccount.Text;
            string displayName = tbNameAccount.Text;
            int type = (int)cbAccountType.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDelAccount_Click(object sender, EventArgs e)
        {
            string userName = tbAccountAccount.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = tbAccountAccount.Text;
            string displayName = tbNameAccount.Text;
            int type = (int)cbAccountType.Value;

            EditAccount(userName, displayName, type);
        }

        private void BtnViewAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnChangePasswordAccount_Click(object sender, EventArgs e)
        {
            string userName = tbAccountAccount.Text;

            ResetPass(userName);
        }

        private void f_Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QuanLyQuanCafeDataSet.USP_GetListBill' table. You can move, or remove it, as needed.
            this.USP_GetListBillTableAdapter.Fill(this.QuanLyQuanCafeDataSet.USP_GetListBill);
            // TODO: This line of code loads data into the 'QuanLyQuanCafeDataSet.USP_Tablefood' table. You can move, or remove it, as needed.
            // this.USP_TablefoodTableAdapter.Fill(this.QuanLyQuanCafeDataSet.USP_Tablefood);


            this.reportViewer1.RefreshReport();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = tbNameCategory.Text;

            AddCatagory(name);
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = tbNameTable.Text;
            if(name != "")
            {
                AddTable(name);
            }
            else
            {
                MessageBox.Show("thất bại");
            }
          
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = tbNameTable.Text;
            int id = Convert.ToInt32(tbIdTable.Text);

            TableDAO.Instance.EditTable(name, id);
            MessageBox.Show("Sửa ban thành công");
            LoadTable();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = tbNameCategory.Text;
            //int categoryID = (cbCategoryFood.SelectedItem as Category).ID;
            //float price = (float)nmPriceFood.Value;
            int id = Convert.ToInt32(tbIDCategory.Text);

            if (CategoryDAO.Instance.editCategory(name,id))
            {
                MessageBox.Show("thành công");
                LoadCategory();
              
            }
            else
            {
                MessageBox.Show("Có lỗi");
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
