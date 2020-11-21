using QuanLiQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static int tableWidth = 80;
        public static int tableHeight = 80;

        public static TableDAO Instance
        {
            get => (instance == null) ? new TableDAO() : instance;
            private set => instance = value;
        }

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("dbo.USP_Tablefood");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
