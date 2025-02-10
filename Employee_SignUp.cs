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
    public partial class Employee_SignUp : Form
    {
        public Employee_SignUp()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Goodness Pharmacy\Goodness_pharm.mdf"";Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Get the values to be inserted from your Windows Form controls
                    int id = Convert.ToInt32(bunifuTextBox4.Text);
                    string firstName = bunifuTextBox1.Text;
                    string lastName = bunifuTextBox2.Text;
                    string nic = bunifuTextBox3.Text;
                    string address = bunifuTextBox7.Text;
                    string gender = bunifuDropdown1.SelectedItem?.ToString(); // Validate selected item
                    int phoneNo = Convert.ToInt32(bunifuTextBox5.Text);
                    string username = bunifuTextBox8.Text;
                    string password = bunifuTextBox6.Text;
                    string confirm_Password = bunifuTextBox10.Text;
                    string email = bunifuTextBox9.Text;
                    DateTime hireDate = bunifuDatePicker2.Value;
                    DateTime dob = bunifuDatePicker1.Value;
                    string jobTitle = bunifuTextBox13.Text;
                    string otherDetails = bunifuTextBox14.Text;

                    // Perform form validation
                    if (password != confirm_Password)
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
                        string.IsNullOrEmpty(confirm_Password) ||
                        string.IsNullOrEmpty(jobTitle))
                    {
                        MessageBox.Show("Please fill in all the required fields.");
                        return; // Stop further execution
                    }

                    // Create the SQL insert query
                    string query = "INSERT INTO Employeesign (Id, First_Name, Last_Name, NIC, Gender, Address, Phone_No, Username, Password, Confirm_Password, Email, DOB, Hire_Date, Job_Title, Other_Details) " +
                        "VALUES (@Id, @FirstName, @LastName, @NIC, @Gender, @Address, @Phone_No, @Username, @Password, @Confirm_Password, @Email, @DOB, @HireDate, @JobTitle, @OtherDetails)";

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
                        command.Parameters.AddWithValue("@Confirm_Password", confirm_Password);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@DOB", dob);
                        command.Parameters.AddWithValue("@HireDate", hireDate);
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

                    // Hide the current form and show the login form
                    this.Hide();

                    Employee_Login emp = new Employee_Login();
                    emp.Show();
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
