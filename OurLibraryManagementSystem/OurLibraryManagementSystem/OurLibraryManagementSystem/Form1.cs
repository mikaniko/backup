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
            this.Shown += show;  
      
        }
        private void show(object sender, EventArgs e)
        {
            AutoShowTheDashboard();
        }
        public void AutoShowTheDashboard()
        {
            panel2.Controls.Clear();

            theDashboard dashboardControl = new theDashboard(); 
            dashboardControl.Dock = DockStyle.Fill;
            panel2.Controls.Add(dashboardControl);
           
        }
        private void ResetSidebarButtonStyles()
        {
            Button[] buttons = {
        btnDashboard,
        btnBookInventory,
        btnBorrowReturn,
        btnBorrower,
        btnReports,
       
    };

            foreach (Button btn in buttons)
            {
                btn.BackColor = Color.LightGray;
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            foreach (Button btn in buttons)
            {
                btn.Width = 180;
                btn.Height = 40;
                btn.Margin = new Padding(20, 10, 20, 0); 
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.White;
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btn.UseVisualStyleBackColor = false;
            }
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.MouseOverBackColor = Color.White;
            btnLogout.TextAlign = ContentAlignment.MiddleCenter;
            btnLogout.UseVisualStyleBackColor = false;  
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);   
        }
        private void HighlightSidebarButton(Button activeBtn)
        {
            ResetSidebarButtonStyles();

            activeBtn.BackColor = Color.White;
            activeBtn.ForeColor = Color.Blue ;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }



        private void ShowDashboard(theDashboard dashboard)
        {
            splitContainer1.Panel2.Controls.Clear();

          
            dashboard.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(dashboard);
            dashboard.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard(new theDashboard());
            HighlightSidebarButton(btnDashboard);

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
            HighlightSidebarButton(btnBookInventory);
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
            HighlightSidebarButton(btnBorrowReturn);

        }




        private void ShowBorrowManagement(theBorrowerManagement form)
        {
            splitContainer1.Panel2.Controls.Clear();


            form.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }
        private void btnBorrower_Click(object sender, EventArgs e)
        {
            ShowBorrowManagement(new theBorrowerManagement());
            HighlightSidebarButton(btnBorrower);

        }
        private void ShowReports(theReports form)
        {
            splitContainer1.Panel2.Controls.Clear();


            form.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            HighlightSidebarButton(btnReports);
            ShowReports(new theReports());


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

 

   
