using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using static OurLibraryManagementSystem.LibraryData;

namespace OurLibraryManagementSystem
{
    public partial class theBorrowReturn : UserControl
    {
        private enum FormMode
        {
            None,
            Borrow,
            Return
        }
        private FormMode currentMode = FormMode.None;


        public theBorrowReturn()
        {
            InitializeComponent();
            InitializeFormState();
            InitializeConditionComboBox(); // Add this line
        }


        private void InitializeFormState()
        {
            // Initially disable both forms
            SetBorrowFormEnabled(false);
            SetReturnFormEnabled(false);

            // Set current mode to None
            currentMode = FormMode.None;

            // Update UI state
            UpdateUIState();
        }

        private void SetBorrowFormEnabled(bool enabled)
        {
            // Enable/disable all borrow form controls
            // Replace these with your actual control names
            tbBorrowID.Enabled = enabled;
            tbBorrowerName.Enabled = enabled;
            tbBookIDBorrow.Enabled = enabled;
            tbBookTitle.Enabled = enabled;
            siticoneBorrowDateTimePicker1.Enabled = enabled;
            siticoneDueDateTimePicker2.Enabled = enabled;
            btnBorrowSubmit.Enabled = enabled;
        }

        private void SetReturnFormEnabled(bool enabled)
        {
            // Enable/disable all return form controls
            // Replace these with your actual control names
            tbReturnBookID.Enabled = enabled;
            tbReturnBorrowID.Enabled = enabled;
            siticoneReturnDateTimePicker3.Enabled = enabled;
            cbCondition.Enabled = enabled;
            btnReturnSubmit.Enabled = enabled;
        }

        private void UpdateUIState()
        {
            // Update button appearances based on current mode
            switch (currentMode)
            {
                case FormMode.Borrow:
                    btnBorrowBook.ShadowDecoration.Color = Color.Green;
                    btnReturnBook.ShadowDecoration.Color = Color.Gray;
                    btnBorrowBook.FillColor = Color.LightGreen;
                    btnReturnBook.FillColor = Color.LightGray;
                    break;

                case FormMode.Return:
                    btnBorrowBook.ShadowDecoration.Color = Color.Gray;
                    btnReturnBook.ShadowDecoration.Color = Color.Green;
                    btnBorrowBook.FillColor = Color.LightGray;
                    btnReturnBook.FillColor = Color.LightGreen;
                    break;

                case FormMode.None:
                    btnBorrowBook.ShadowDecoration.Color = Color.Gray;
                    btnReturnBook.ShadowDecoration.Color = Color.Gray;
                    btnBorrowBook.FillColor = Color.LightGray;
                    btnReturnBook.FillColor = Color.LightGray;
                    break;
            }
        }












        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (currentMode == FormMode.Borrow)
            {
                // If already in borrow mode, toggle off
                currentMode = FormMode.None;
                SetBorrowFormEnabled(false);
            }
            else
            {
                // Switch to borrow mode
                currentMode = FormMode.Borrow;
                SetBorrowFormEnabled(true);
                SetReturnFormEnabled(false);
            }

            UpdateUIState();

        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            if (currentMode == FormMode.Return)
            {
                // If already in return mode, toggle off
                currentMode = FormMode.None;
                SetReturnFormEnabled(false);
            }
            else
            {
                // Switch to return mode
                currentMode = FormMode.Return;
                SetReturnFormEnabled(true);
                SetBorrowFormEnabled(false);
            }

            UpdateUIState();
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrowSubmit_Click(object sender, EventArgs e)
        {
            if (currentMode != FormMode.Borrow)
            {
                MessageBox.Show("Please select Borrow mode first.", "Invalid Operation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Your borrow submission logic here
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(tbBorrowID.Text))
                {
                    MessageBox.Show("Please enter a Borrow ID!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tbBookIDBorrow.Text))
                {
                    MessageBox.Show("Please enter a Book ID!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tbBorrowerName.Text))
                {
                    MessageBox.Show("Please enter a Borrower Name!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(tbBookTitle.Text))
                {
                    MessageBox.Show("Please enter a Book Title!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // FIXED: Proper date validation instead of checking Text property
                DateTime borrowDate = siticoneBorrowDateTimePicker1.Value;
                DateTime dueDate = siticoneDueDateTimePicker2.Value;
                DateTime today = DateTime.Today;
 
                // Validate due date is after borrow date
                if (dueDate <= borrowDate)
                {
                    MessageBox.Show("Due date must be after the borrow date!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate due date is not too far in the future (optional)
                if (dueDate > today.AddYears(1)) // Example: max 1 year
                {
                    MessageBox.Show("Due date cannot be more than 1 year from now!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ProcessReturnTransaction();
                MessageBox.Show("Book returned successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearReturnForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning book: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturnSubmit_Click(object sender, EventArgs e)
        {
            if (currentMode != FormMode.Return)
            {
                MessageBox.Show("Please select Return mode first.", "Invalid Operation",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Your borrow submission logic here
            try
            {
                if (currentMode != FormMode.Return)
                {
                    MessageBox.Show("Please select Return mode first.", "Invalid Operation",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Validate inputs
                    if (string.IsNullOrWhiteSpace(tbReturnBookID.Text))
                    {
                        MessageBox.Show("Please enter a Book ID!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(tbReturnBorrowID.Text))
                    {
                        MessageBox.Show("Please enter a Borrower ID!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cbCondition.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a Condition of the Book!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validate return date
                    DateTime returnDate = siticoneReturnDateTimePicker3.Value;
                    DateTime today = DateTime.Today;

                    if (siticoneReturnDateTimePicker3.Value.Date == DateTime.Today)
                    {
                        ProcessReturnTransaction();
                        MessageBox.Show("Book returned successfully!");
                        ClearReturnForm();
                    }
                    else
                    {
                        MessageBox.Show("Date must be today!");
                    }

                 
                    


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error returning book: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

           private bool ValidateReturnForm()
        {
            // Add your validation logic here
            if (string.IsNullOrEmpty(tbReturnBookID.Text))
            {
                MessageBox.Show("Please enter Book ID", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Add more validations as needed
            return true;
        }

        private void ProcessBorrowTransaction()
        {
            // Your borrow processing logic
            // Example: Save to database, update records, etc.
        }

        private void ProcessReturnTransaction()
        {
            // Your return processing logic
            // Example: Update database, calculate fines, etc.
        }

        private void ClearBorrowForm()
        {
            // Clear borrow form fields
            tbBorrowID.Clear();
            tbBorrowerName.Clear();
            tbBookIDBorrow.Clear();
            tbBookTitle.Clear();
            siticoneBorrowDateTimePicker1.Value = DateTime.Now;
            siticoneDueDateTimePicker2.Value = DateTime.Now.AddDays(14); // Example: 14 days due
        }

        private void ClearReturnForm()
        {
            // Clear return form fields but keep return date auto-filled
            tbReturnBookID.Clear();
            tbReturnBorrowID.Clear();
            cbCondition.SelectedIndex = -1; // Reset condition selection

       
        }

        // Reset both forms (optional - can be called when switching between modes)
        public void ResetForms()
        {
            currentMode = FormMode.None;
            SetBorrowFormEnabled(false);
            SetReturnFormEnabled(false);
            ClearBorrowForm();
            ClearReturnForm();
            UpdateUIState();
        }

        private void InitializeConditionComboBox()
        {
            cbCondition.Items.Clear();
            cbCondition.Items.Add("Good");
            cbCondition.Items.Add("Damaged");
            cbCondition.Items.Add("Lost");
            cbCondition.SelectedIndex = -1; // No selection by default
        }

        private void siticoneBorrowDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void siticoneDueDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void siticoneReturnDateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
   
