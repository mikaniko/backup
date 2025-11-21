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
    public partial class theBorrowerManagement : UserControl
    {
        public theBorrowerManagement()
        {
            InitializeComponent();
            LoadBorrowers();

        }
        private void LoadBorrowers()
        {
            siticoneDataGridView1.Rows.Clear();
            foreach (var borrower in LibraryData.Borrowers)
            {
                siticoneDataGridView1.Rows.Add(borrower.StudentID, borrower.Name,
                                              borrower.ContactNumber, borrower.Email,
                                              borrower.BorrowedBooksCount, borrower.Status);
            }
        }

        private void btnAddBorrower_Click(object sender, EventArgs e)
        {
            // Similar to Add Book form, but for borrowers
            // USER CAN ADD NEW BORROWERS
        }

        private void theBorrowerManagement_Load(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

