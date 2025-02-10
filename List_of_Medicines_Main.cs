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
    public partial class List_of_Medicines_Main : Form
    {
        public List_of_Medicines_Main()
        {
            InitializeComponent();
            linkLabel2.Text = Program.UserRole + " - Sign Out";
            bunifuLabel3.Text = Program.UserName;
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            Add_Medicine addmed = new Add_Medicine();
            addmed.Show();
            this.Close();
        }

        private void bunifuButton218_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Close();
        }

        private void bunifuButton217_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
            this.Close();
        }

        private void bunifuButton216_Click(object sender, EventArgs e)
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

        private void bunifuButton215_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Close();
        }

        private void bunifuButton214_Click(object sender, EventArgs e)
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

        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            Purchase purch = new Purchase();
            purch.Show();
            this.Close();
        }

        private void bunifuButton212_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void bunifuButton211_Click(object sender, EventArgs e)
        {
            Technical_Support tech = new Technical_Support();
            tech.Show();
            this.Close();
        }

        private void bunifuButton210_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will close the application. Do you wish to proceed?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "Sign Out", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Login_and_Signup logsign = new Login_and_Signup();
                logsign.Show();
                this.Hide();

            }
        }
        public void LoadMedicineData()
        {
            // Code to load medicine data into the DataGridView
            // ...

            // Example code:
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL select query
                    string query = "SELECT * FROM AddMedicine";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a SqlDataAdapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Set the DataGridView's data source
                        bunifuDataGridView1.DataSource = dataTable;

                        foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
                        {
                            if (row.Cells["Quantity"].Value != null && Convert.ToInt32(row.Cells["Quantity"].Value) < 5)
                            {
                                //highlight the row
                                row.DefaultCellStyle.BackColor = Color.Brown;
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.SelectionBackColor = Color.Brown;
                                row.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                        }

                        connection.Close();
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

        private int currentIndex = 0;
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (currentIndex < bunifuDataGridView1.Rows.Count - 1)
            {
                // Increment the current index to move to the next record
                currentIndex++;

                // Display the data of the next record in the DataGridView
                bunifuDataGridView1.CurrentCell = bunifuDataGridView1.Rows[currentIndex].Cells[0];
            }
        }

        private void List_of_Medicines_Main_Load(object sender, EventArgs e)
        {
            LoadMedicineData();
        }
    }

}
