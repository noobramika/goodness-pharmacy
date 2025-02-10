using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Goodness_Pharmacy
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
            linkLabel1.Text = Program.UserRole + " - Sign Out";
            bunifuLabel2.Text = Program.UserName;
        }



        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Goodness Pharmacy\Goodness_pharm.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Open the connection here

                    // Get the values to be inserted from your Windows Form controls
                    int saleCode = Convert.ToInt32(bunifuTextBox3.Text);
                    string customerName = bunifuDropdownCustomer.SelectedItem?.ToString();
                    DateTime date = bunifuDatePickerDate.Value;
                    string medicine = bunifuDropdownMedicine.SelectedItem?.ToString();
                    int quantity = Convert.ToInt32(bunifuTextBoxQuantity.Text);
                    string notes = bunifuTextBoxNotes.Text;
                    float discount = Convert.ToSingle(bunifuTextBoxDiscount.Text);
                    string payment = bunifuDropdownPayment.SelectedItem?.ToString();
                    float discountPercentage = Convert.ToSingle(bunifuTextBoxDiscount.Text) / 100;

                    // Perform form validation
                    if (string.IsNullOrEmpty(customerName) ||
                        string.IsNullOrEmpty(medicine) ||
                        string.IsNullOrEmpty(payment))
                    {
                        MessageBox.Show("Customer, Medicine, and Payment options are required");
                        return; // Stop further execution
                    }

                    // Check if the selected quantity is available in the AddMedicine table
                    string availableQuantityQuery = "SELECT Quantity FROM AddMedicine WHERE Medicine_Name = @Medicine_Name";

                    using (SqlCommand availableQuantityCommand = new SqlCommand(availableQuantityQuery, connection))
                    {
                        availableQuantityCommand.Parameters.AddWithValue("@Medicine_Name", medicine);

                        // Execute the command and retrieve the available quantity
                        object availableQuantityResult = availableQuantityCommand.ExecuteScalar();

                        if (availableQuantityResult != null)
                        {
                            int availableQuantity = Convert.ToInt32(availableQuantityResult);

                            if (quantity > availableQuantity)
                            {
                                MessageBox.Show("Selected quantity is not available in stock.");
                                return; // Stop further execution
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected medicine is not found in stock.");
                            return; // Stop further execution
                        }
                    }

                    // Calculate the grand total for the current item
                    float sellPrice = 0.0f; // Initialize with a default value
                    string sellPriceQuery = "SELECT Sell_Price FROM AddMedicine WHERE Medicine_Name = @Medicine_Name";

                    using (SqlCommand sellPriceCommand = new SqlCommand(sellPriceQuery, connection))
                    {
                        sellPriceCommand.Parameters.AddWithValue("@Medicine_Name", medicine);

                        // Execute the command and retrieve the sell price
                        object sellPriceResult = sellPriceCommand.ExecuteScalar();

                        if (sellPriceResult != null)
                        {
                            sellPrice = Convert.ToSingle(sellPriceResult);
                        }
                    }

                    float discountAmount = sellPrice * discountPercentage;
                    float grandTotal = quantity * (sellPrice - discountAmount);

                    // Create the SQL insert query without the grand total parameter
                    string query = "INSERT INTO Sales (Sale_Code, Customer_Name, Date, Medicine, Quantity, Notes, Discount, Grand_Total, Payment, Sell_Price) " +
                                   "VALUES (@SaleCode, @CustomerName, @Date, @Medicine, @Quantity, @Notes, @Discount, @GrandTotal, @Payment, @SellPrice)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@SaleCode", saleCode);
                        command.Parameters.AddWithValue("@CustomerName", customerName);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@Medicine", medicine);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Notes", notes);
                        command.Parameters.AddWithValue("@Discount", discount);
                        command.Parameters.AddWithValue("@GrandTotal", grandTotal);
                        command.Parameters.AddWithValue("@Payment", payment);
                        command.Parameters.AddWithValue("@SellPrice", sellPrice);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    // Add the values to the DataGridView as a new row
                    bunifuDataGridView1.Rows.Add(medicine, saleCode, quantity, grandTotal);

                    // Calculate the total of all items with discounts
                    float totalWithDiscounts = 0.0f;
                    foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
                    {
                        float rowGrandTotal = Convert.ToSingle(row.Cells[3].Value);
                        totalWithDiscounts += rowGrandTotal;
                    }

                    // Update the total label with the calculated total with discounts
                    totalLabel.Text = totalWithDiscounts.ToString();

                    // Reduce the available quantity in the AddMedicine table
                    string reduceQuantityQuery = "UPDATE AddMedicine SET Quantity = Quantity - @Quantity " +
                                                 "WHERE Medicine_Name = @Medicine_Name";

                    using (SqlCommand reduceQuantityCommand = new SqlCommand(reduceQuantityQuery, connection))
                    {
                        reduceQuantityCommand.Parameters.AddWithValue("@Quantity", quantity);
                        reduceQuantityCommand.Parameters.AddWithValue("@Medicine_Name", medicine);

                        reduceQuantityCommand.ExecuteNonQuery();
                    }

                    connection.Close(); // Close the connection here
                }

                MessageBox.Show("Data inserted successfully!");
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

        private void bunifuButton28_Click(object sender, EventArgs e)
        {
            Technical_Support tech = new Technical_Support();
            tech.Show();
            this.Close();
        }

        private void bunifuButton29_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Select_supplier_sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected customer from the dropdown
            string selectedCustomer = bunifuDropdownCustomer.SelectedItem?.ToString();

            // Check if a customer is selected
            if (!string.IsNullOrEmpty(selectedCustomer))
            {
                // Perform actions based on the selected customer
                // For example, you can display additional information about the customer or retrieve related data from the database.
                MessageBox.Show("Customer: " + selectedCustomer);
            }
        }


        private void Sales_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL select query
                    string query = "SELECT Name FROM Customer";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear the existing items in the dropdown textbox
                            bunifuDropdownCustomer.Items.Clear();

                            // Loop through the results and add each customer name to the dropdown
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                bunifuDropdownCustomer.Items.Add(name);
                            }
                        }

                        // Close the connection
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
            // Code for populating the dropdown textbox with added medicine names
            

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL select query
                    string query = "SELECT Medicine_Name FROM AddMedicine";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the query and get the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear the existing items in the dropdown textbox
                            bunifuDropdownMedicine.Items.Clear();

                            // Loop through the results and add each medicine name to the dropdown
                            while (reader.Read())
                            {
                                string medicineName = reader.GetString(0);
                                bunifuDropdownMedicine.Items.Add(medicineName);
                            }
                        }

                        // Close the connection
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

        private void bunifuDropdownMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected medicine name from the dropdown
            string selectedMedicine = bunifuDropdownMedicine.SelectedItem?.ToString();

            // Check if a medicine name is selected
            if (!string.IsNullOrEmpty(selectedMedicine))
            {
                // Perform actions based on the selected medicine name
                // For example, you can display additional information about the medicine or retrieve related data from the database.
                MessageBox.Show("Medicine: " + selectedMedicine);
            }
        }

        private void bunifuButton210_Click(object sender, EventArgs e)
        {
            // Check if there are any rows in the DataGridView
            if (bunifuDataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("There are no items in the cart.");
                return;
            }

            // Create a StringBuilder to store the receipt text
            StringBuilder receiptBuilder = new StringBuilder();

            // Append header information to the receipt
            receiptBuilder.AppendLine("Goodness Pharmacy");
            receiptBuilder.AppendLine("Receipt");
            receiptBuilder.AppendLine("--------------------------");
            receiptBuilder.AppendLine($"Date: {DateTime.Now}");
            receiptBuilder.AppendLine();

            float total = 0.0f; // Cumulative total variable

            // Iterate through the rows of the DataGridView to generate item details
            foreach (DataGridViewRow row in bunifuDataGridView1.Rows)
            {
                // Retrieve the selected row from the DataGridView
                if (row.Cells[0].Value != null)
                {
                    string medicine = row.Cells[0].Value.ToString();
                    int saleCode = Convert.ToInt32(row.Cells[1].Value);
                    int quantity = Convert.ToInt32(row.Cells[2].Value);
                    float grandTotal = Convert.ToSingle(row.Cells[3].Value);

                    total += grandTotal; // Add grand total to the cumulative total

                    receiptBuilder.AppendLine($"Medicine: {medicine}");
                    receiptBuilder.AppendLine($"Sale Code: {saleCode}");
                    receiptBuilder.AppendLine($"Quantity: {quantity}");
                    receiptBuilder.AppendLine("--------------------------");
                }
            }

            receiptBuilder.AppendLine($"Total: {total}"); // Display the cumulative total

            // Display the generated receipt
            MessageBox.Show(receiptBuilder.ToString(), "Receipt");

            // Create the PrintDocument instance
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Print the document
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Retrieve the receipt text from the StringBuilder
            string receiptText = totalLabel.ToString();

            // Set the font, margins, and format for the printing
            Font font = new Font("Arial", 10);
            Margins margins = new Margins(50, 50, 50, 50);
            RectangleF printArea = new RectangleF(
                margins.Left, margins.Top,
                e.PageBounds.Width - margins.Left - margins.Right,
                e.PageBounds.Height - margins.Top - margins.Bottom);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            // Print the receipt text
            e.Graphics.DrawString(receiptText, font, Brushes.Black, printArea, format);
        }


        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Close();
        }
    }
}
