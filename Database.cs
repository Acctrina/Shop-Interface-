using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OOPG_MiniProject
{
    class Database
    {
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\darkc\source\repos\OOPG_MiniProject\OOPG_MiniProject\Database1.mdf;Integrated Security=True";

        SqlConnection connObj;
        SqlCommand comdObj;
        SqlDataReader dR;

        public Database()
        {
            connObj = new SqlConnection(connStr);
            connObj.Open();
        }

        public SqlDataReader ExecuteReader(string selStr)
        {
            comdObj = new SqlCommand(selStr, connObj);
            dR = comdObj.ExecuteReader();
            return dR;
        }

        public int ExecuteNonQuery(string sqlStr)
        {
            comdObj = new SqlCommand(sqlStr, connObj);
            return comdObj.ExecuteNonQuery();
        }

    }
}
