using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OurLibraryManagementSystem.LibraryData;

namespace OurLibraryManagementSystem
{
    public partial class theBookInventory : UserControl
    {
        private List<Book> filteredBooks = new List<Book>();

        // Delegate for search operations (as per handout)
        public delegate void SearchPerformedHandler(string searchTerm);
        public event SearchPerformedHandler SearchPerformed;


        public theBookInventory()
        {
            InitializeComponent();
            InitializeDataGridView(); // Initialize columns FIRST
            LoadBooks(); // Then load data
            LoadAutocompleteSuggestions();

            // Subscribe to events
            LibraryData.BooksUpdated += LoadBooks;
        }
        private void InitializeDataGridView()
        {
            // Clear existing columns to avoid duplicates
            siticoneDataGridView1.Columns.Clear();

            // Add your data columns
            siticoneDataGridView1.Columns.Add("BookID", "Book ID");
            siticoneDataGridView1.Columns.Add("Title", "Title");
            siticoneDataGridView1.Columns.Add("Author", "Author");
            siticoneDataGridView1.Columns.Add("Category", "Category");
            siticoneDataGridView1.Columns.Add("Status", "Status");
            siticoneDataGridView1.Columns.Add("DateAdded", "Date Added");


            // Add Edit button column
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "📝 Edit",
                Text = "📝", // Edit icon
                UseColumnTextForButtonValue = true,
                Width = 50,
            };
            siticoneDataGridView1.Columns.Add(editColumn);

            // Add Delete button column
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "🗑 Delete",
                Text = "🗑", // Delete icon
                UseColumnTextForButtonValue = true,
                Width = 50,
            };

            siticoneDataGridView1.Columns.Add(deleteColumn);

            // Configure DataGridView appearance - FIXED SETTINGS
            siticoneDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            siticoneDataGridView1.ReadOnly = true;
            siticoneDataGridView1.AllowUserToAddRows = false;


        }
        private void LoadBooks()
        {
            //   siticoneDataGridView1.Rows.Clear();
            // foreach (var book in LibraryData.Books)
            //{
            //   siticoneDataGridView1.Rows.Add(book.BookID, book.Title, book.Author,
            // book.Category, book.Status, book.DateAdded.ToShortDateString());

            // DisplayBooks(LibraryData.Books);


            DisplayBooks(LibraryData.Books);

            try
            {
                // Using List<T> collection as per generic collections handout
                DisplayBooks(new List<Book>(LibraryData.Books));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DisplayBooks(List<Book> books)
        {
            siticoneDataGridView1.Rows.Clear();
            filteredBooks = books;



            foreach (var book in books)
            {
                siticoneDataGridView1.Rows.Add(
                    book.BookID,
                    book.Title,
                    book.Author,
                    book.Category,
                    book.Status,
                    book.DateAdded.ToShortDateString()
                );
            }



            // Force refresh and ensure visibility
            siticoneDataGridView1.Refresh();
            siticoneDataGridView1.Visible = true;
        }




        private void SiticoneDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }






        //for hint in search bar
        private void LoadAutocompleteSuggestions()
        {
            try
            {
                AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();

                // Add all book properties to suggestions
                foreach (var book in LibraryData.Books)
                {
                    if (!string.IsNullOrWhiteSpace(book.Title))
                        suggestions.Add(book.Title);
                    if (!string.IsNullOrWhiteSpace(book.Author))
                        suggestions.Add(book.Author);
                    if (!string.IsNullOrWhiteSpace(book.Category))
                        suggestions.Add(book.Category);
                    if (!string.IsNullOrWhiteSpace(book.BookID))
                        suggestions.Add(book.BookID);
                }

                tbSearch.AutoCompleteCustomSource = suggestions;
                tbSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tbSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suggestions: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }






        private void roundedTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            try
            {
                AddBook addForm = new AddBook();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks(); // Refresh the list
                    LoadAutocompleteSuggestions(); // Update suggestions

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void siticoneDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //     if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // var bookId = siticoneDataGridView1.Rows[e.RowIndex].Cells["BookID"].Value?.ToString();
            //  var book = LibraryData.FindBook(bookId);

            //if (book == null) return;

            //if (e.ColumnIndex == siticoneDataGridView1.Columns["Edit"].Index)
            // {
            //    EditBook(book);
            // }
            //  else if (e.ColumnIndex == siticoneDataGridView1.Columns["Delete"].Index)
            //{
            //    DeleteBook(book);
            //}


            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var bookId = siticoneDataGridView1.Rows[e.RowIndex].Cells["BookID"].Value?.ToString();
                var book = LibraryData.FindBook(bookId);

                if (book == null) return;

                if (e.ColumnIndex == siticoneDataGridView1.Columns["Edit"].Index)
                {
                    EditBook(book);
                }
                else if (e.ColumnIndex == siticoneDataGridView1.Columns["Delete"].Index)
                {
                    DeleteBook(book);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing action: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            }


        private void EditBook(Book book)
        {
            try
            {
                AddBook editForm = new AddBook(book);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing book: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void DeleteBook(Book book)
        {
            if (MessageBox.Show($"Are you sure you want to delete '{book.Title}'?", "Confirm Delete",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LibraryData.RemoveBook(book.BookID);
                ActivityLogger.LogActivity($"Deleted book: {book.Title}", "Book");
                LoadBooks();
                LoadAutocompleteSuggestions();


                MessageBox.Show("Book deleted successfully!", "Success",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            SearchBooks(tbSearch.Text);
        
            }
        


        private void SearchBooks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadBooks();
                return;
            }

            var filteredBooks = LibraryData.Books.Where(book =>
                    (book.Title ?? "").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (book.Author ?? "").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (book.Category ?? "").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (book.BookID ?? "").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (book.Status ?? "").IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            DisplayBooks(filteredBooks);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            tbSearch.Clear();
            LoadBooks();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Ensure DataGridView is properly sized and visible
            if (siticoneDataGridView1 != null)
            {
                siticoneDataGridView1.BringToFront();
                siticoneDataGridView1.Visible = true;

                // Force layout update
                this.PerformLayout();
                siticoneDataGridView1.PerformLayout();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            LibraryData.BooksUpdated -= LoadBooks;
            base.OnHandleDestroyed(e);
        }
    }
}


