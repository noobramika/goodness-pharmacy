using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goodness_Pharmacy
{
    public partial class Login_and_Signup : Form
    {
        public Login_and_Signup()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will close the application. Do you wish to proceed?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
        }


        private void bunifuButton21_Click_1(object sender, EventArgs e)
        {
            Admin_Login adlog = new Admin_Login();
            adlog.Show();
            
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            Employee_Login emplog = new Employee_Login();
            
            emplog.Show();
            
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            Employee_SignUp empsignup = new Employee_SignUp();
            empsignup.Show();
            
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            Admin_Signup adminsignup = new Admin_Signup();
            adminsignup.Show();
            
        }
    }
}
