using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurLibraryManagementSystem
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ShowDashboard(theDashboard form)
        {
            splitContainer1.Panel2.Controls.Clear();

          
            form.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard(new theDashboard());  
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }




        private void ShowBookInventory(theBookInventory form)
        {
            splitContainer1.Panel2.Controls.Clear();


            form.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }
      
        private void btnBookInventory_Click(object sender, EventArgs e)
        {
            ShowBookInventory(new theBookInventory());
        }


        private void ShowBorrowReturn(theBorrowReturn form)
        {
            splitContainer1.Panel2.Controls.Clear();


            form.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }
        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {
            ShowBorrowReturn(new theBorrowReturn());
        }





        private void btnBorrower_Click(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        private void panelist_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

 

   
