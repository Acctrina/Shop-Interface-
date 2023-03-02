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

namespace OOPG_MiniProject
{
    public partial class Shop : Form
    {
        int custID, salesID;
        string Qty, membership;
        SqlDataReader dR;
        Transactions transObj;
        Login loginObj;
        Database dbObj = new Database();

        public Shop()
        {
            InitializeComponent();
        }
        public Shop(string value)       
        {
            InitializeComponent();
            this.value = value;
        }
        public string value { get; set; }
        private void Shop_Load(object sender, EventArgs e)
        {          
                nameLabel.Text = value;
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (mouseCheckBox.Checked)
                {
                    Qty = mouseqtyComboBox.Text;
                    checkoutListBox.Items.Add(Qty + " Computer Mouse");
                }
                if(pchargerCheckBox.Checked)
                {
                    Qty = pchargerqtyComboBox.Text;
                    checkoutListBox.Items.Add(Qty + " Portable Charger");
                }
                if (earpCheckBox.Checked)
                {
                    Qty = earpqtyComboBox.Text;
                    checkoutListBox.Items.Add(Qty + " Earphones");
                }
                if (usbCheckBox.Checked)
                {
                    Qty = usbqtyComboBox.Text;
                    checkoutListBox.Items.Add(Qty + " USB-C Cable");
                }
            }
            catch
            {
                MessageBox.Show("Please select an item!");
            }
            addButton.Enabled = false;
            checkOutButton.Focus();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            checkoutListBox.Items.Clear();
            mouseCheckBox.Checked = false;
            pchargerCheckBox.Checked = false;
            earpCheckBox.Checked = false;
            usbCheckBox.Checked = false;

            addButton.Enabled = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pastransButton_Click(object sender, EventArgs e)
        {

            Transactions frm = new Transactions(nameLabel.Text);
            frm.ShowDialog();

            transObj = new Transactions();
            transObj.Show();
        }

        private void checkOutButton_Click(object sender, EventArgs e)
        {
            float mouseprice = 0;
            float pchargerprice = 0;
            float earpprice = 0;
            float usbcprice = 0;

            string selectStr = "Select CustID From CustomerTBL where Name = " + "'" + value + "'";
            dR = dbObj.ExecuteReader(selectStr);

            if (dR.Read())
            {
                custID = int.Parse(dR["CustID"].ToString());

                if (mouseCheckBox.Checked)
                {
                    salesID = 3;
                    int intQty = int.Parse(mouseqtyComboBox.Text);

                    mouseprice = 15 * intQty;

                    string strInsert = "Insert into TransactionTBL(Date, Quantity, CustID, SalesID, Price)" + " Values('" + DateTime.Today.ToString("MM / dd / yyyy")
                                         + "'," + intQty + "," + custID + "," + salesID + "," + mouseprice + ")";
                    Database dB = new Database();
                    int row = dB.ExecuteNonQuery(strInsert);

                    dR.Close();

                }
                if (pchargerCheckBox.Checked)
                {
                    salesID = 4;
                    int intQty = int.Parse(pchargerqtyComboBox.Text);
                   
                    pchargerprice = 20 * intQty;

                    string strInsert = "Insert into TransactionTBL(Date, Quantity, CustID, SalesID, Price)" + " Values('" + DateTime.Today.ToString("MM/dd/yyyy")
                                         + "'," + intQty + "," + custID + "," + salesID + "," + pchargerprice + ")";
                    Database dB = new Database();
                    int row = dB.ExecuteNonQuery(strInsert);
                }
                if (earpCheckBox.Checked)
                {
                    salesID = 5;
                    int intQty = int.Parse(earpqtyComboBox.Text);
                 
                    earpprice = 25 * intQty;

                    string strInsert = "Insert into TransactionTBL(Date, Quantity, CustID, SalesID, Price)" + " Values('" + DateTime.Today.ToString("MM/dd/yyyy")
                                         + "'," + intQty + "," + custID + "," + salesID + "," + earpprice + ")";
                    Database dB = new Database();
                    int row = dB.ExecuteNonQuery(strInsert);
                }
                if (usbCheckBox.Checked)
                {
                    salesID = 6;
                    int intQty = int.Parse(usbqtyComboBox.Text);
                  
                    usbcprice = 10 * intQty;

                    string strInsert = "Insert into TransactionTBL(Date, Quantity, CustID, SalesID, Price)" + " Values('" + DateTime.Today.ToString("MM/dd/yyyy")
                                         + "'," + intQty + "," + custID + "," + salesID + "," + usbcprice + ")";
                    Database dB = new Database();
                    int row = dB.ExecuteNonQuery(strInsert);
                }
               
            }
            dR.Close();

            Checkout chkoutOBj = new Checkout();
            Membership membObj = new Membership();

            string selectMembership = "Select Membership From CustomerTBL where Name = " + "'" + value + "'";
            dR = dbObj.ExecuteReader(selectMembership);

            if (dR.Read())
            {
                membership = dR["Membership"].ToString();

                if(membership == "Yes")
                {
                    membObj.CalTotalCost(mouseprice + pchargerprice + earpprice + usbcprice);
                    MessageBox.Show("Please pay a total of $" + membObj.TotalCost + " (20% Discount)\nThank You for Shopping with us.");
                }
                if(membership == "No")
                {
                    chkoutOBj.CalTotalCost(mouseprice + pchargerprice + earpprice + usbcprice);
                    MessageBox.Show("Please pay a total of $" + chkoutOBj.TotalCost + "\nThank You for Shopping with us.");
                }
            }
            dR.Close();
           
            
                
        }

    }
}
