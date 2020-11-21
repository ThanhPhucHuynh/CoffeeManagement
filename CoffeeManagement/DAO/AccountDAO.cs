using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance { get => (instance == null)?new AccountDAO() : instance; 
            private set => instance = value; }
        
        private  AccountDAO() { }

        public bool check_Login(string userName, string password)
        {

            string query = "USP_Login @username , @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName,password});

            return result.Rows.Count > 0;
        }
    }
}
