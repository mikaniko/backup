using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OurLibraryManagementSystem.LibraryData;

namespace OurLibraryManagementSystem
{
    public partial class theDashboard : UserControl
    {
        //  private Label lblTotalBooksValue, lblBorrowedBooksValue, lblOverdueBooksValue, lblTotalBorrowersValue;
        private FlowLayoutPanel recentActivityPanel;



        public theDashboard()
        {
            InitializeComponent();
            LoadStatistics();
            CreateRecentActivitySection();
            LoadRecentActivities();


            // Listen for data changes
            LibraryData.StatisticsUpdated += LoadStatistics;
            ActivityLogger.ActivityAdded += LoadRecentActivities;


        }

        private void CreateRecentActivitySection()
        {
            panelRecentActivity = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White
            };

            // Add to your existing layout where you want recent activities to appear
            // You'll need to adjust your dashboard layout to include this panel
        }
        private void LoadRecentActivities()
        {
            if (panelRecentActivity.InvokeRequired)
            {
                panelRecentActivity.Invoke(new Action(LoadRecentActivities));
                return;
            }

            panelRecentActivity.Controls.Clear();

            foreach (var activity in ActivityLogger.Activities)
            {
                var activityItem = new Panel
                {
                    Width = panelRecentActivity.Width - 25,
                    Height = 60,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                var lblDescription = new Label
                {
                    Text = activity.Description,
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                var lblTime = new Label
                {
                    Text = activity.Timestamp.ToString("MMM dd, yyyy hh:mm tt"),
                    Font = new Font("Segoe UI", 8, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(10, 30),
                    AutoSize = true
                };

                activityItem.Controls.Add(lblDescription);
                activityItem.Controls.Add(lblTime);
                panelRecentActivity.Controls.Add(activityItem);
            }
        }




        private void label8_Click(object sender, EventArgs e)
        {

        }



        private void btnAddNewBook_Click(object sender, EventArgs e)
        {
            AddBook addForm = new AddBook();
            addForm.Show();


            RefreshStatistics(); // Update dashboard stats
        }

        private void theDashboard_Load(object sender, EventArgs e)
        {

        }

        private void LoadStatistics()
        {
            if (lblTotalBooks != null)
            {
                lblTotalBooks.Text = LibraryData.Books.Count.ToString();
            }

            // Count borrowed books - will be 0 until user borrows some
            int borrowedBooks = LibraryData.Books.FindAll(book => book.Status == "Borrowed").Count;
            txtBorrowedBooks.Text = borrowedBooks.ToString();

            // Count overdue books
            int overdueBooks = LibraryData.Books.FindAll(book => book.Status == "Overdue").Count;
            txtOverdueBooks.Text = overdueBooks.ToString();

            // Count active borrowers
            int activeBorrowers = LibraryData.Borrowers.FindAll(borrower => borrower.Status == "Active").Count;
            txtBorrowers.Text = activeBorrowers.ToString();


        }


        // Don't forget to unsubscribe when control is disposed
        protected override void OnHandleDestroyed(EventArgs e)
        {
            LibraryData.StatisticsUpdated -= LoadStatistics;
            ActivityLogger.ActivityAdded -= LoadRecentActivities;

            base.OnHandleDestroyed(e);
        }



        public void RefreshStatistics()
        {
            LoadStatistics(); // Call this when user adds/removes data
        }

        // Add the UI creation methods from previous code...
    }
}