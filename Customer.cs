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

namespace Goodness_Pharmacy
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            linkLabel1.Text = Program.UserRole + " - Sign Out";
            bunifuLabel2.Text = Program.UserName;
        }

        private void bunifuButton210_Click(object sender, EventArgs e)
        {
            Manage_Customers manage = new Manage_Customers();
            manage.Show();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Close();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            if(Program.UserRole == "Admin")
            {
                Sales_Report salesrep = new Sales_Report();
                salesrep.Show();
                this.Close();
            }
            else
                MessageBox.Show("You do not have permission to access this");
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
            Purchase purch = new Purchase();
            purch.Show();
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            Technical_Support tech = new Technical_Support();
            tech.Show();
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will close the application. Do you wish to proceed?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Code to close the application
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login_and_Signup logsign = new Login_and_Signup();
            logsign.Show();
            this.Close();
        }

        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be inserted from your Windows Form controls
                    int id = Convert.ToInt32(bunifuTextBoxCustomerId.Text);
                    string name = bunifuTextBoxCustomerName.Text;
                    string address = bunifuTextBoxCustomerAddress.Text;
                    int phone = Convert.ToInt32(bunifuTextBoxCustomerPhone.Text);

                    // Create the SQL insert query
                    string query = "INSERT INTO customer (Id, Name, Address, Phone) " +
                                   "VALUES (@Id, @Name, @Address, @Phone)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        // Display a success message or perform any additional tasks
                        MessageBox.Show("Customer data inserted successfully!");
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

        }

        private void bunifuButton211_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be updated from your Windows Form controls
                    int id = Convert.ToInt32(bunifuTextBoxCustomerId.Text);

                    // Create the SQL update query
                    string query = "UPDATE customer SET ";

                    // Create a list to store the parameters and their corresponding values
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    // Check if the customer name field is filled
                    if (!string.IsNullOrEmpty(bunifuTextBoxCustomerName.Text))
                    {
                        query += "Name = @Name, ";
                        parameters.Add(new SqlParameter("@Name", bunifuTextBoxCustomerName.Text));
                    }

                    // Check if the customer address field is filled
                    if (!string.IsNullOrEmpty(bunifuTextBoxCustomerAddress.Text))
                    {
                        query += "Address = @Address, ";
                        parameters.Add(new SqlParameter("@Address", bunifuTextBoxCustomerAddress.Text));
                    }

                    // Check if the customer phone field is filled
                    if (!string.IsNullOrEmpty(bunifuTextBoxCustomerPhone.Text))
                    {
                        int phone = Convert.ToInt32(bunifuTextBoxCustomerPhone.Text);
                        query += "Phone = @Phone, ";
                        parameters.Add(new SqlParameter("@Phone", phone));
                    }

                    // Remove the trailing comma and space from the query
                    query = query.TrimEnd(',', ' ');

                    // Add the WHERE clause for the ID
                    query += " WHERE Id = @Id";
                    parameters.Add(new SqlParameter("@Id", id));

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameters to the command
                        command.Parameters.AddRange(parameters.ToArray());

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Display a success message or perform any additional tasks
                            MessageBox.Show("Customer data updated successfully!");

                            // Call the method to update the DataGridView in the "manage_customers" form
                            Manage_Customers manageCustomersForm = Application.OpenForms["ManageCustomersForm"] as Manage_Customers;
                            manageCustomersForm?.LoadCustomerData();
                        }
                        else
                        {
                            MessageBox.Show("No customer found with the specified ID.");
                        }
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

        }

        private void bunifuButton212_Click(object sender, EventArgs e)
        {

            try
            {
                // Get the customer ID from your Windows Form control
                int id = Convert.ToInt32(bunifuTextBoxCustomerId.Text);

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL delete query
                    string query = "DELETE FROM Customer WHERE Id = @Id";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the customer ID as a parameter to the query
                        command.Parameters.AddWithValue("@Id", id);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Display a success message or perform any additional tasks
                            MessageBox.Show("Customer data deleted successfully!");

                            // Call the method to update the DataGridView in the "manage_purchase" form
                            Manage_Purchases managePurchaseForm = Application.OpenForms["ManagePurchaseForm"] as Manage_Purchases;
                            managePurchaseForm?.LoadPurchaseData();
                        }
                        else
                        {
                            MessageBox.Show("No customer found with the specified ID.");
                        }
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

        }
    }
}
