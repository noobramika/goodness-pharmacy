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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Goodness_Pharmacy
{
    public partial class Admin_Signup : Form
    {
        public Admin_Signup()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Goodness_Pharmacy\\Goodness_pharm.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be inserted from your Windows Form controls
                    int id = Convert.ToInt32(bunifuTextBoxDelete.Text);
                    string firstName = bunifuTextBox1.Text;
                    string lastName = bunifuTextBox2.Text;
                    string nic = bunifuTextBox3.Text;
                    string address = bunifuTextBox7.Text;
                    string gender = bunifuDropdown1.SelectedItem?.ToString(); // Validate selected item
                    int phoneNo = Convert.ToInt32(bunifuTextBox5.Text);
                    string username = bunifuTextBox8.Text;
                    string password = bunifuTextBoxPasswordDelete.Text;
                    string confirmPassword = bunifuTextBox10.Text;
                    string email = bunifuTextBox9.Text;
                    DateTime hireDate = bunifuDatePicker2.Value;
                    DateTime dob = bunifuDatePicker1.Value;
                    string jobTitle = bunifuTextBox13.Text;
                    string otherDetails = bunifuTextBox14.Text;

                    // Perform form validation
                    if (password != confirmPassword)
                    {
                        MessageBox.Show("Password and Confirm Password do not match.");
                        return; // Stop further execution
                    }

                    if (string.IsNullOrEmpty(firstName) ||
                        string.IsNullOrEmpty(nic) ||
                        string.IsNullOrEmpty(address) ||
                        string.IsNullOrEmpty(gender) ||
                        string.IsNullOrEmpty(username) ||
                        string.IsNullOrEmpty(password) ||
                        string.IsNullOrEmpty(confirmPassword) ||
                        string.IsNullOrEmpty(jobTitle))
                    {
                        MessageBox.Show("Please fill in all the required fields.");
                        return; // Stop further execution
                    }

                    // Create the SQL insert query
                    string query = "INSERT INTO adminsignup (Id, First_Name, Last_Name, NIC, Gender, Address, Phone_No, Username, Password, Confirm_Password, Email, DOB, Hire_Date, Job_Title, Other_Details) " +
                        "VALUES (@Id, @FirstName, @LastName, @NIC, @Gender, @Address, @Phone_No, @Username, @Password, @ConfirmPassword, @Email, @DOB, @HireDate, @JobTitle, @OtherDetails)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@NIC", nic);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone_No", phoneNo);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@ConfirmPassword", confirmPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@HireDate", hireDate);
                        command.Parameters.AddWithValue("@DOB", dob);
                        command.Parameters.AddWithValue("@JobTitle", jobTitle);
                        command.Parameters.AddWithValue("@OtherDetails", otherDetails);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }

                    // Display a success message or perform any additional tasks
                    MessageBox.Show("Data inserted successfully!");
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

            this.Hide();

            Admin_Login admin = new Admin_Login();
            admin.Show();



        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Goodness_Pharmacy\\Goodness_pharm.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be updated from your Windows Form controls
                    int id;
                    if (!int.TryParse(bunifuTextBoxDelete.Text, out id))
                    {
                        MessageBox.Show("Invalid ID. Please enter a valid integer value.");
                        return; // Stop further execution
                    }

                    // Create the SQL update query
                    string query = "UPDATE adminsignup SET";

                    List<string> updateFields = new List<string>();
                    List<string> parameters = new List<string>();

                    if (!string.IsNullOrEmpty(bunifuTextBox1.Text))
                    {
                        updateFields.Add("First_Name = @FirstName");
                        parameters.Add("@FirstName");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox2.Text))
                    {
                        updateFields.Add("Last_Name = @LastName");
                        parameters.Add("@LastName");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox3.Text))
                    {
                        updateFields.Add("NIC = @NIC");
                        parameters.Add("@NIC");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox7.Text))
                    {
                        updateFields.Add("Address = @Address");
                        parameters.Add("@Address");
                    }

                    if (bunifuDropdown1.SelectedItem != null)
                    {
                        updateFields.Add("Gender = @Gender");
                        parameters.Add("@Gender");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox5.Text))
                    {
                        updateFields.Add("Phone_No = @Phone_No");
                        parameters.Add("@Phone_No");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox8.Text))
                    {
                        updateFields.Add("Username = @Username");
                        parameters.Add("@Username");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBoxPasswordDelete.Text))
                    {
                        updateFields.Add("Password = @Password");
                        parameters.Add("@Password");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox10.Text))
                    {
                        updateFields.Add("Confirm_Password = @ConfirmPassword");
                        parameters.Add("@ConfirmPassword");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox9.Text))
                    {
                        updateFields.Add("Email = @Email");
                        parameters.Add("@Email");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox13.Text))
                    {
                        updateFields.Add("Job_Title = @JobTitle");
                        parameters.Add("@JobTitle");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox14.Text))
                    {
                        updateFields.Add("Other_Details = @OtherDetails");
                        parameters.Add("@OtherDetails");
                    }

                    if (updateFields.Count == 0)
                    {
                        MessageBox.Show("No fields to update.");
                        return; // Stop further execution
                    }

                    query += " " + string.Join(", ", updateFields);
                    query += " WHERE Id = @Id";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", id);

                        for (int i = 0; i < parameters.Count; i++)
                        {
                            string parameter = parameters[i];
                            string parameterValue = "";

                            switch (parameter)
                            {
                                case "@FirstName":
                                    parameterValue = bunifuTextBox1.Text;
                                    break;
                                case "@LastName":
                                    parameterValue = bunifuTextBox2.Text;
                                    break;
                                case "@NIC":
                                    parameterValue = bunifuTextBox3.Text;
                                    break;
                                case "@Address":
                                    parameterValue = bunifuTextBox7.Text;
                                    break;
                                case "@Gender":
                                    parameterValue = bunifuDropdown1.SelectedItem.ToString();
                                    break;
                                case "@Phone_No":
                                    parameterValue = bunifuTextBox5.Text;
                                    break;
                                case "@Username":
                                    parameterValue = bunifuTextBox8.Text;
                                    break;
                                case "@Password":
                                    parameterValue = bunifuTextBoxPasswordDelete.Text;
                                    break;
                                case "@ConfirmPassword":
                                    parameterValue = bunifuTextBox10.Text;
                                    break;
                                case "@Email":
                                    parameterValue = bunifuTextBox9.Text;
                                    break;
                                case "@JobTitle":
                                    parameterValue = bunifuTextBox13.Text;
                                    break;
                                case "@OtherDetails":
                                    parameterValue = bunifuTextBox14.Text;
                                    break;
                            }

                            command.Parameters.AddWithValue(parameter, parameterValue);
                        }

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }

                    // Get the instance of the admin_Details form
                    admin_Details adminDetailsForm = Application.OpenForms["admin_Details"] as admin_Details;

                    // Check if the form instance exists
                    if (adminDetailsForm != null)
                    {
                        // Call the RefreshDataGridView method of the admin_Details form to update the DataGridView
                        adminDetailsForm.RefreshDataGridView();
                    }

                    // Display a success message or perform any additional tasks
                    MessageBox.Show("Data updated successfully!");
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

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Goodness_Pharmacy\\Goodness_pharm.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the ID and password from your Windows Form controls
                    int idToDelete = Convert.ToInt32(bunifuTextBoxDelete.Text);
                    string passwordToDelete = bunifuTextBoxPasswordDelete.Text;

                    // Create the SQL delete query with additional condition for the password
                    string query = "DELETE FROM adminsignup WHERE Id = @Id AND Password = @Password";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", idToDelete);
                        command.Parameters.AddWithValue("@Password", passwordToDelete);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Display a success message or perform any additional tasks
                            MessageBox.Show("Record deleted successfully!");
                        }
                        else
                        {
                            // Display a message if no record was found with the provided ID and password
                            MessageBox.Show("No record found with the provided ID and password.");
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

        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the ID and password from your Windows Form controls
                    int idToDelete = Convert.ToInt32(bunifuTextBoxDelete.Text);
                    string passwordToDelete = bunifuTextBoxPasswordDelete.Text;

                    // Create the SQL delete query with additional condition for the password
                    string query = "DELETE FROM adminsignup WHERE Id = @Id AND Password = @Password";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", idToDelete);
                        command.Parameters.AddWithValue("@Password", passwordToDelete);

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            // Display a success message or perform any additional tasks
                            MessageBox.Show("Record deleted successfully!");
                        }
                        else
                        {
                            // Display a message if no record was found with the provided ID and password
                            MessageBox.Show("No record found with the provided ID and password.");
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

        private void bunifuButton22_Click_1(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be updated from your Windows Form controls
                    int id;
                    if (!int.TryParse(bunifuTextBoxDelete.Text, out id))
                    {
                        MessageBox.Show("Invalid ID. Please enter a valid integer value.");
                        return; // Stop further execution
                    }

                    // Create the SQL update query
                    string query = "UPDATE adminsignup SET";

                    List<string> updateFields = new List<string>();
                    List<string> parameters = new List<string>();

                    if (!string.IsNullOrEmpty(bunifuTextBox1.Text))
                    {
                        updateFields.Add("First_Name = @FirstName");
                        parameters.Add("@FirstName");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox2.Text))
                    {
                        updateFields.Add("Last_Name = @LastName");
                        parameters.Add("@LastName");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox3.Text))
                    {
                        updateFields.Add("NIC = @NIC");
                        parameters.Add("@NIC");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox7.Text))
                    {
                        updateFields.Add("Address = @Address");
                        parameters.Add("@Address");
                    }

                    if (bunifuDropdown1.SelectedItem != null)
                    {
                        updateFields.Add("Gender = @Gender");
                        parameters.Add("@Gender");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox5.Text))
                    {
                        updateFields.Add("Phone_No = @Phone_No");
                        parameters.Add("@Phone_No");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox8.Text))
                    {
                        updateFields.Add("Username = @Username");
                        parameters.Add("@Username");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBoxPasswordDelete.Text))
                    {
                        updateFields.Add("Password = @Password");
                        parameters.Add("@Password");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox10.Text))
                    {
                        updateFields.Add("Confirm_Password = @ConfirmPassword");
                        parameters.Add("@ConfirmPassword");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox9.Text))
                    {
                        updateFields.Add("Email = @Email");
                        parameters.Add("@Email");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox13.Text))
                    {
                        updateFields.Add("Job_Title = @JobTitle");
                        parameters.Add("@JobTitle");
                    }

                    if (!string.IsNullOrEmpty(bunifuTextBox14.Text))
                    {
                        updateFields.Add("Other_Details = @OtherDetails");
                        parameters.Add("@OtherDetails");
                    }

                    if (updateFields.Count == 0)
                    {
                        MessageBox.Show("No fields to update.");
                        return; // Stop further execution
                    }

                    query += " " + string.Join(", ", updateFields);
                    query += " WHERE Id = @Id";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection and set their values
                        command.Parameters.AddWithValue("@Id", id);

                        for (int i = 0; i < parameters.Count; i++)
                        {
                            string parameter = parameters[i];
                            string parameterValue = "";

                            switch (parameter)
                            {
                                case "@FirstName":
                                    parameterValue = bunifuTextBox1.Text;
                                    break;
                                case "@LastName":
                                    parameterValue = bunifuTextBox2.Text;
                                    break;
                                case "@NIC":
                                    parameterValue = bunifuTextBox3.Text;
                                    break;
                                case "@Address":
                                    parameterValue = bunifuTextBox7.Text;
                                    break;
                                case "@Gender":
                                    parameterValue = bunifuDropdown1.SelectedItem.ToString();
                                    break;
                                case "@Phone_No":
                                    parameterValue = bunifuTextBox5.Text;
                                    break;
                                case "@Username":
                                    parameterValue = bunifuTextBox8.Text;
                                    break;
                                case "@Password":
                                    parameterValue = bunifuTextBoxPasswordDelete.Text;
                                    break;
                                case "@ConfirmPassword":
                                    parameterValue = bunifuTextBox10.Text;
                                    break;
                                case "@Email":
                                    parameterValue = bunifuTextBox9.Text;
                                    break;
                                case "@JobTitle":
                                    parameterValue = bunifuTextBox13.Text;
                                    break;
                                case "@OtherDetails":
                                    parameterValue = bunifuTextBox14.Text;
                                    break;
                            }

                            command.Parameters.AddWithValue(parameter, parameterValue);
                        }

                        // Open the connection
                        connection.Open();

                        // Execute the query
                        command.ExecuteNonQuery();

                        // Close the connection
                        connection.Close();
                    }

                    // Get the instance of the admin_Details form
                    admin_Details adminDetailsForm = Application.OpenForms["admin_Details"] as admin_Details;

                    // Check if the form instance exists
                    if (adminDetailsForm != null)
                    {
                        // Call the RefreshDataGridView method of the admin_Details form to update the DataGridView
                        adminDetailsForm.RefreshDataGridView();
                    }

                    // Display a success message or perform any additional tasks
                    MessageBox.Show("Data updated successfully!");
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
