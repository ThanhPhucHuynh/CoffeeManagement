using CoffeeManagement.DAO;
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
        public f_Admin()
        {

            InitializeComponent();  

            LoadAccountList();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        void LoadAccountList()
        {

            string query = "EXEC USP_GetAccountByUserName @userName";
            
            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query,new object[] {"staff" });

        }
        void LoadFoodList()
        {

            string query = "select * from food";

            dtgwFood.DataSource = DataProvider.Instance.ExecuteQuery(query);

        }
    }
}
