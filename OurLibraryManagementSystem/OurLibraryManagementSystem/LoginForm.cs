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
            // Assuming your panel is named panel1
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



        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact system administrator to reset your password.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {

            if (txtUsername.Text == "lib" && txtPassword.Text == "lib1")
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

        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {

            if (txtUsername.Text == "lib" && txtPassword.Text == "lib1")
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
    }
    
}
