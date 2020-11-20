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

            string connectionsSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionsSTR);
            string query = "SELECT * FROM Account";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);
            connection.Close();

            dtgvAccount.DataSource = data;

        }   
    }
}
