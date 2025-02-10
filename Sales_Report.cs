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
    public partial class Sales_Report : Form
    {
        public Sales_Report()
        {
            InitializeComponent();
            linkLabel1.Text = Program.UserRole + " - Sign Out";
            bunifuLabel2.Text = Program.UserName;

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
            this.Close();
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
            this.Close();
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
            this.Close();
        }

        private void bunifuButton27_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            Technical_Support tech = new Technical_Support();
            tech.Show();
            this.Close();
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

        private void Sales_Report_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            // Define the SELECT query to fetch all data from the Sales table
            string selectQuery = "SELECT * FROM Sales";

            // Create a DataTable to hold the retrieved data
            DataTable salesData = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SqlDataAdapter to execute the SELECT query and fill the DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);

                    // Open the connection
                    connection.Open();

                    // Fill the DataTable with the data from the Sales table
                    adapter.Fill(salesData);

                    // Close the connection
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions that occurred during the database operations
                MessageBox.Show("An error occurred while retrieving sales data: " + ex.Message);
                return;
            }

            // Bind the DataTable to the DataGridView
            dataGridViewSales.DataSource = salesData;






        }


        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            // Get the filter values from the TextBoxes
            string customerNameFilter = bunifuTextBox1.Text.Trim();
            string salesCodeFilterText = bunifuTextBoxQuantity.Text.Trim();

            // Parse the sales code filter value if it's a valid integer
            int? salesCodeFilter = null;
            if (!string.IsNullOrEmpty(salesCodeFilterText))
            {
                int parsedSalesCode;
                if (int.TryParse(salesCodeFilterText, out parsedSalesCode))
                {
                    salesCodeFilter = parsedSalesCode;
                }
            }

            // Apply the filters to the DataTable bound to the DataGridView
            DataTable salesData = dataGridViewSales.DataSource as DataTable;

            if (salesData != null)
            {
                // Construct the filter expression based on the filter values
                string filterExpression = "";

                if (!string.IsNullOrEmpty(customerNameFilter))
                {
                    // Add the customer name filter condition
                    filterExpression += $"[Customer_Name] LIKE '%{customerNameFilter}%' AND ";
                }

                if (salesCodeFilter.HasValue)
                {
                    // Add the sales code filter condition
                    filterExpression += $"[Sale_Code] = {salesCodeFilter.Value} AND ";
                }

                // Remove the trailing "AND" if there are filter conditions
                if (!string.IsNullOrEmpty(filterExpression))
                {
                    filterExpression = filterExpression.Remove(filterExpression.Length - 5);
                }

                // Apply the filter to the DataTable
                salesData.DefaultView.RowFilter = filterExpression;

                // Check if any records match the filter
                if (salesData.DefaultView.Count == 0)
                {
                    MessageBox.Show("No records found matching the filter criteria.");
                }
            }
        }




    }
}
