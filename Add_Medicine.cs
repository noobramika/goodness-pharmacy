using Bunifu.UI.WinForms;
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
    public partial class Add_Medicine : Form
    {
        public Add_Medicine()
        {
            InitializeComponent();
            linkLabel2.Text = Program.UserRole + " - Sign Out";
            bunifuLabel3.Text = Program.UserName;
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
            if (Program.UserRole == "Admin")
            {
            Sales_Report salesrep = new Sales_Report();
            salesrep.Show();
            this.Close();
        }
            else
                MessageBox.Show("You do not have permission to access this");
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
            Purchase purchase = new Purchase();
            purchase.Show();
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
            Technical_Support technical = new Technical_Support();
            technical.Show();
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

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be inserted from your Windows Form controls
                    int id = Convert.ToInt32(bunifuTextBoxPurchaseId.Text);
                    string medicineName = bunifuTextBoxMedicineName.Text;
                    DateTime expireDate = bunifuDatePickerExpireDate.Value;
                    string medicineGroup = bunifuTextBox1.Text;
                    string quantity = bunifuTextBoxQuantity.Text;
                    string boxSize = bunifuTextBoxBoxSize.Text;
                    string howToUse = bunifuTextBoxHowToUse.Text;
                    float sellPrice = Convert.ToSingle(bunifuSellPrice.Text);
                    float supplierPrice = Convert.ToSingle(bunifuTextBoxSupplierPrice.Text);
                    string category = bunifuTextBox2.Text;

                    // Perform form validation
                    if (string.IsNullOrEmpty(medicineName) ||
                        string.IsNullOrEmpty(quantity) ||
                        string.IsNullOrEmpty(boxSize) ||
                        string.IsNullOrEmpty(howToUse) ||
                        string.IsNullOrEmpty(category) ||
                        string.IsNullOrEmpty(medicineGroup))
                    {
                        MessageBox.Show("Please fill in all the required fields.");
                        return; // Stop further execution
                    }

                    // Create the SQL insert query
                    string query = "INSERT INTO AddMedicine (Id, Medicine_Name, Expire_Date, Medicine_Group, Quantity, Box_Size, How_to_use, Sell_Price, Supplier_Price, category) " +
                                   "VALUES (@Id, @MedicineName, @ExpireDate, @MedicineGroup, @Quantity, @BoxSize, @HowToUse, @SellPrice, @SupplierPrice, @Category)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@MedicineName", medicineName);
                        command.Parameters.AddWithValue("@ExpireDate", expireDate);
                        command.Parameters.AddWithValue("@MedicineGroup", medicineGroup);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@BoxSize", boxSize);
                        command.Parameters.AddWithValue("@HowToUse", howToUse);
                        command.Parameters.AddWithValue("@SellPrice", sellPrice);
                        command.Parameters.AddWithValue("@SupplierPrice", supplierPrice);
                        command.Parameters.AddWithValue("@Category", category);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // After saving the data into the "AddMedicine" table
                        // ...

                        List_of_Medicines_Main medicineDetailsForm = Application.OpenForms["List_of_Medicines_Main"] as List_of_Medicines_Main;
                        medicineDetailsForm?.LoadMedicineData();


                        // Close the connection
                        connection.Close();

                        // Display a success message or perform any additional tasks
                        MessageBox.Show("Data inserted successfully!");
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
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
     
            try
            {
                // Get the values to be updated from your Windows Form controls
                int id = Convert.ToInt32(bunifuTextBoxPurchaseId.Text);
                string medicineName = bunifuTextBoxMedicineName.Text;
                DateTime expireDate = bunifuDatePickerExpireDate.Value;
                string medicineGroup = bunifuTextBox1.Text;
                string quantity = bunifuTextBoxQuantity.Text;
                string boxSize = bunifuTextBoxBoxSize.Text;
                string howToUse = bunifuTextBoxHowToUse.Text;
                float sellPrice = Convert.ToSingle(bunifuSellPrice.Text);
                float supplierPrice = Convert.ToSingle(bunifuTextBoxSupplierPrice.Text);
                string category = bunifuTextBox2.Text;

                // Perform form validation
                if (string.IsNullOrEmpty(medicineName) ||
                    string.IsNullOrEmpty(quantity) ||
                    string.IsNullOrEmpty(boxSize) ||
                    string.IsNullOrEmpty(howToUse) ||
                    string.IsNullOrEmpty(category) ||
                    string.IsNullOrEmpty(medicineGroup))
                {
                    MessageBox.Show("Please fill in all the required fields.");
                    return; // Stop further execution
                }

                // Create the SQL update query
                string query = "UPDATE AddMedicine SET Medicine_Name = @MedicineName, Expire_Date = @ExpireDate, Medicine_Group = @MedicineGroup, " +
                               "Quantity = @Quantity, Box_Size = @BoxSize, How_to_use = @HowToUse, Sell_Price = @SellPrice, Supplier_Price = @SupplierPrice, " +
                               "Category = @Category WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@MedicineName", medicineName);
                        command.Parameters.AddWithValue("@ExpireDate", expireDate);
                        command.Parameters.AddWithValue("@MedicineGroup", medicineGroup);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@BoxSize", boxSize);
                        command.Parameters.AddWithValue("@HowToUse", howToUse);
                        command.Parameters.AddWithValue("@SellPrice", sellPrice);
                        command.Parameters.AddWithValue("@SupplierPrice", supplierPrice);
                        command.Parameters.AddWithValue("@Category", category);
                        command.Parameters.AddWithValue("@Id", id);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update the DataGridView in the Medicine_Details_Main form
                            List_of_Medicines_Main medicineDetailsForm = Application.OpenForms["MedicineDetailsForm"] as List_of_Medicines_Main;
                            medicineDetailsForm?.LoadMedicineData();

                            MessageBox.Show("Data updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated.");
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

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            List_of_Medicines_Main list_Of_ = new List_of_Medicines_Main();
            list_Of_.Show();
            this.Close();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            

            try
            {
                // Get the purchase ID from your Windows Form control
                int id = Convert.ToInt32(bunifuTextBoxPurchaseId.Text);

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the SQL delete query
                    string query = "DELETE FROM AddMedicine WHERE Id = @Id";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the purchase ID as a parameter to the query
                        command.Parameters.AddWithValue("@Id", id);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        // Display a success message or perform any additional tasks
                        MessageBox.Show("Purchase data deleted successfully!");

                        // Call the method to update the DataGridView in the "manage_purchase" form
                        List_of_Medicines_Main managePurchaseForm = Application.OpenForms["ManagePurchaseForm"] as List_of_Medicines_Main;
                        managePurchaseForm?.LoadMedicineData();
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
