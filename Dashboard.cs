using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goodness_Pharmacy
{
    public partial class Dashboard : Form
    {
       

        public Dashboard()
        {
            InitializeComponent();
            linkLabel1.Text = Program.UserRole+" - Sign Out";
            bunifuLabel2.Text = Program.UserName;

            //dashboardstatistics
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";
            string query1 = "SELECT COUNT(*) AS TotalSuppliers FROM Supplier";
            string query2 = "SELECT COUNT(*) AS TotalAdmins FROM adminsignup";
            string query3 = "SELECT COUNT(*) AS TotalEmp FROM Employeesign";
            string query4 = "SELECT COUNT(*) AS TotalCus FROM Customer";
            string query5 = "SELECT TOP 1 Medicine FROM Sales GROUP BY Medicine ORDER BY COUNT(*) DESC";
            string freqMedicine = null;
            string query6 = "SELECT COUNT(*) AS TotalMed FROM AddMedicine";
            string query7 = "SELECT COUNT(DISTINCT Medicine_Group) AS NoTypes FROM AddMedicine";
            string query8 = "SELECT SUM(Quantity) AS TotalSoldQuantity FROM Sales";
            string query9 = "SELECT COUNT(*) AS TotalSales FROM Sales";
            int totalSoldQuantity = 0;
            //1
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query1, con))
                    {
                        con.Open();
                        int totalSuppliers = (int)command.ExecuteScalar();
                        supLabel.Text = totalSuppliers.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //2
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query2, con))
                    {
                        con.Open();
                        int totalAdmins = (int)command.ExecuteScalar();
                        admlabel.Text = "Admins: " + totalAdmins.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //3
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query3, con))
                    {
                        con.Open();
                        int TotalEmp = (int)command.ExecuteScalar();
                        emplabel.Text = "Employees: " + TotalEmp.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //4
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query4, con))
                    {
                        con.Open();
                        int TotalCus = (int)command.ExecuteScalar();
                        cuslabel.Text = TotalCus.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //5
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query5, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                freqMedicine = reader.GetString(0);
                            }
                        }
                        medLabel.Text = freqMedicine.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exception
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //6
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query6, con))
                    {
                        con.Open();
                        int TotalMed = (int)command.ExecuteScalar();
                        totalmedlabel.Text = TotalMed.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //7
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query7, con))
                    {
                        con.Open();
                        int NoTypes = (int)command.ExecuteScalar();
                        medGrplabel.Text = NoTypes.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //8
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query8, con))
                    {
                        con.Open();
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalSoldQuantity = Convert.ToInt32(result);
                        }
                        medsaleslabel.Text = totalSoldQuantity.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
            //9
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query9, con))
                    {
                        con.Open();
                        int TotalSales = (int)command.ExecuteScalar();
                        saleslabel.Text = TotalSales.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An SQL exception occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message);
            }
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will close the application. Do you wish to proceed?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "Sign Out", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Login_and_Signup logsign = new Login_and_Signup();
                logsign.Show();
                this.Hide();
                
            }



        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
            
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            if (Program.UserRole == "Admin")
            {
                Sales_Report salesrep = new Sales_Report();
                salesrep.Show();
                this.Close();
            }
            else
                MessageBox.Show("You do not have permission to access this");

        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            if (Program.UserRole == "Admin")
            {
                Supplier supplier = new Supplier();
                supplier.Show();
                this.Close();
            }
            else
                MessageBox.Show("You do not have permission to access this");
        }

        private void bunifuButton26_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Show();
            
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            Technical_Support technical = new Technical_Support();
            technical.Show();

        }

        private void bunifuButton210_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void bunifuButton211_Click(object sender, EventArgs e)
        {
            if (Program.UserRole == "Admin")
            {
                Sales_Report salesrep = new Sales_Report();
                salesrep.Show();
                this.Close();
            }
            else
                MessageBox.Show("You do not have permission to access this");
        }

        private void bunifuButton212_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Show();
        }

        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            List_of_Medicines_Main list_Of_ = new List_of_Medicines_Main();
            list_Of_.Show();
        }

        private void bunifuButton215_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void bunifuButton217_Click(object sender, EventArgs e)
        {

            Technical_Support tech = new Technical_Support();
            tech.Show();
            this.Close();
        }

        private void bunifuButton216_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }

       
    }

}
