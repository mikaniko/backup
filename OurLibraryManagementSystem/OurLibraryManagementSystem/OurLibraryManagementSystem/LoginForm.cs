using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace OurLibraryManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

        }
        
       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            int x = (this.ClientSize.Width - panel1.Width) / 2;
            int y = (this.ClientSize.Height - panel1.Height) / 2;
            panel1.Location = new Point(x, y);

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {

            if (lblUsername.Text == "lib" && lblPassword.Text == "lib1")
            {

                Form1 mainForm = new Form1();
                mainForm.Show();

            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            this.Hide();
        }


        private void button_Click1(object sender, EventArgs e)
        {

            if (lblUsername.Text == "lib" && lblPassword.Text == "lib1")
            {

                // Initialize sample data
                LibraryData.InitializeSampleData();

                Form1 mainForm = new Form1();
                mainForm.Show();
              // mainForm.AutoShowTheDashboard();
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.Hide();

        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact system administrator to reset your password.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void theDashboard1_Load(object sender, EventArgs e)
        {
            theDashboard1.Hide();
        }
    }
    
}
