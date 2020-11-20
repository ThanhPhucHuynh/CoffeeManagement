using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class DataProvider
    {
        
        private string connectionsSTR = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] paramater = null)
        {
            DataTable data = new DataTable();
            using(SqlConnection connection = new SqlConnection(connectionsSTR))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(query, connection);

                if (paramater != null)
                {
                    string[] listPare = query.Split(' ');
                    int i = 0;
                    foreach(string item in listPare)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, paramater[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);
                connection.Close();
            }

            return data;
        }
    }
}
