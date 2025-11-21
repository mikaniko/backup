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
    public partial class AddBook : Form
    {
        public Book NewBook { get; private set; }
        private bool isEditMode = false;
        private Book existingBook;


        public AddBook()
        {
            InitializeComponent();
            Load += AddBook_Load;
            isEditMode = false;
            InitializeForm();

        }
        // Constructor for Edit mode
        public AddBook(Book bookToEdit)
        {
            InitializeComponent();
            isEditMode = true;
            existingBook = bookToEdit;
            InitializeForm();
            PopulateFormWithBookData();
        }

        private void InitializeForm()
        {
            // Set form title and button text based on mode
            this.Text = isEditMode ? "Edit Book" : "Add New Book";

            // Change your "Add" button text to "Update" in edit mode
            // Assuming your add button is called btnAdd or similar
            if (isEditMode)
            {
                // Change the button text from "Add" to "Update"
                foreach (Control control in this.Controls)
                {
                    if (control is Button btn && btn.Text == "Add")
                    {
                        btn.Text = "Update";
                        break;
                    }
                }
            }

            // Populate categories if not already done
            PopulateCategories();
        }


        private void PopulateCategories()
        {
            // Add common categories to your ComboBox
            string[] cbCategories = {
            "Computer Science", "Mathematics", "Physics", "Chemistry",
            "Biology", "Engineering", "Literature", "History",
            "Philosophy", "Psychology", "Business", "Economics",
            "Art", "Music", "Education", "Marketing", "Other"
        };

            // Assuming your category ComboBox is called cbCategory or similar
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox && (comboBox.Name == "cbCategory" || comboBox.Tag?.ToString() == "Category"))
                {
                    comboBox.Items.Clear();
                    comboBox.Items.AddRange(cbCategories);
                    break;
                }
            }
        }
        private void PopulateFormWithBookData()
        {
            if (existingBook != null)
            {
                // Find and populate your textboxes
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        if (textBox.Name == "txtBookID" || textBox.Tag?.ToString() == "BookID")
                        {
                            textBox.Text = existingBook.BookID;
                            textBox.Enabled = false; // Make Book ID read-only in edit mode
                        }
                        else if (textBox.Name == "txtTitle" || textBox.Tag?.ToString() == "Title")
                        {
                            textBox.Text = existingBook.Title;
                        }
                        else if (textBox.Name == "txtAuthor" || textBox.Tag?.ToString() == "Author")
                        {
                            textBox.Text = existingBook.Author;
                        }
                    }
                    else if (control is ComboBox comboBox && (comboBox.Name == "cbCategory" || comboBox.Tag?.ToString() == "Category"))
                    {
                        comboBox.SelectedItem = existingBook.Category;
                    }
                }
            }
        }









        private void siticoneTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void siticoneTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            txtBookID.Text = GenerateBookID();
            if (!isEditMode)
            {
                // Auto-generate Book ID only in Add mode
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox textBox && (textBox.Name == "txtBookID" || textBox.Tag?.ToString() == "BookID"))
                    {
                        textBox.Text = GenerateBookID();
                        break;
                    }
                }
            }
        }


            
        private string GenerateBookID()
        {
            // Auto-generate: BK-0001, BK-0002, etc.
            int nextNumber = LibraryData.Books.Count + 1;
            return $"BK-{nextNumber.ToString("D4")}";
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            SaveBook();
            this.DialogResult = DialogResult.OK;

            if (string.IsNullOrWhiteSpace(txtBookID.Text) ||
                string.IsNullOrWhiteSpace(txtBookID.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(cbCategory.Text))
            {
                return;
            }
            if (isEditMode)
            {
                UpdateBook();
            }
            else
            {
                SaveBook();

            }

        }

        private void UpdateBook()
        {
            string title = "", author = "", category = "";

            // Extract values from form controls
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Name == "txtTitle" || textBox.Tag?.ToString() == "Title")
                    {
                        title = textBox.Text.Trim();
                    }
                    else if (textBox.Name == "txtAuthor" || textBox.Tag?.ToString() == "Author")
                    {
                        author = textBox.Text.Trim();
                    }
                }
                else if (control is ComboBox comboBox && (comboBox.Name == "cbCategory" || comboBox.Tag?.ToString() == "Category"))
                {
                    category = comboBox.SelectedItem?.ToString();
                }
            }

            // Validate inputs
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a Book Title!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Please enter an Author!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Please select a Category!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the existing book
            existingBook.Title = title;
            existingBook.Author = author;
            existingBook.Category = category;

            MessageBox.Show($"Book '{existingBook.Title}' updated successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            ActivityLogger.LogActivity($"Updated book: {existingBook.Title}", "Book");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
    //        if (isDataEntered && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to exit?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.OnFormClosing(e);
        
    }
        
        private void SaveBook()
        {
            string bookId = "", title = "", author = "", category = "";

            // Extract values from form controls
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.Name == "txtBookID" || textBox.Tag?.ToString() == "BookID")
                    {
                        bookId = textBox.Text.Trim();
                    }
                    else if (textBox.Name == "txtTitle" || textBox.Tag?.ToString() == "Title")
                    {
                        title = textBox.Text.Trim();
                    }
                    else if (textBox.Name == "txtAuthor" || textBox.Tag?.ToString() == "Author")
                    {
                        author = textBox.Text.Trim();
                    }
                }
                else if (control is ComboBox comboBox && (comboBox.Name == "cbCategory" || comboBox.Tag?.ToString() == "Category"))
                {
                    category = comboBox.SelectedItem?.ToString();
                }
            }



            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtBookID.Text))
            {
                MessageBox.Show("Please enter a Book ID!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a Book Title!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Please enter an Author!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a Category!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if Book ID already exists
            if (LibraryData.FindBook(txtBookID.Text) != null)
            {
                MessageBox.Show("Book ID already exists! Please use a different ID.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewBook = new Book
            {
                BookID = bookId,
                Title = title,
                Author = author,
                Category = category,
                Status = "Available",
                DateAdded = DateTime.Now
            };

            // Add to library
            LibraryData.AddBook(NewBook);
            ActivityLogger.LogActivity($"Added new book: {NewBook.Title}", "Book");


            MessageBox.Show($"Book '{NewBook.Title}' added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
