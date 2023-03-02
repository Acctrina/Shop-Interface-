using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OOPG_MiniProject
{
    public partial class Transactions : Form
    {
        int custid;
        SqlDataReader dR;
        Database dbObj = new Database();
        public Transactions()
        {
            InitializeComponent();
        }
        public Transactions(string value)
        {
            InitializeComponent();
            this.value = value;
        }
        public string value { get; set; }

        private void Transactions_Load(object sender, EventArgs e)
        {
            
            string selectcustID = "Select CustID From CustomerTBL where Name = " + "'" + value + "'";
            dR = dbObj.ExecuteReader(selectcustID);

            if (dR.Read())
            {
                custid = int.Parse(dR["CustID"].ToString());
                custidLabel.Text = custid.ToString();
            }
            else
            {
                this.Close();
            }
            dR.Close();
         }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\darkc\source\repos\OOPG_MiniProject\OOPG_MiniProject\Database1.mdf;Integrated Security=True");
            con.Open();


            SqlCommand cmd = new SqlCommand("Select * from TransactionTBL where CustID=@custid", con);
            cmd.Parameters.AddWithValue("custid", int.Parse(custidLabel.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();              
        } 
    }
}
