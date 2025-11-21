using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLibraryManagementSystem
{
    public class Book
    {
        public string BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Status { get; set; } = "Available"; // Default: Available
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }

    public class Borrower
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public int BorrowedBooksCount { get; set; } = 0;
        public string Status { get; set; } = "Active"; // Default: Active
    }

    public class Transaction
    {
        public string TransactionID { get; set; }
        public string StudentID { get; set; }
        public string BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Returned, Overdue
    }


    internal class LibraryData
    {
        public static List<Book> Books = new List<Book>();
        public static List<Borrower> Borrowers = new List<Borrower>();
        public static List<Transaction> Transactions = new List<Transaction>();

        // METHODS FOR USER TO MANIPULATE DATA:
        public static event Action BooksUpdated;
        public static event Action BorrowersUpdated;
        public static event Action StatisticsUpdated;

        // BOOK METHODS

        public static void AddBook(Book book)
        {
            Books.Add(book);
            NotifyAllUpdates(); // Trigger all update events

        }

        public static void RemoveBook(string bookId)
        {
            Books.RemoveAll(book => book.BookID == bookId);
            NotifyAllUpdates(); // Trigger all update events

        }
        private static void NotifyAllUpdates()
        {
            BooksUpdated?.Invoke();
            BorrowersUpdated?.Invoke();
            StatisticsUpdated?.Invoke();
        }   

        public static Book FindBook(string bookId)
        {
            return Books.Find(book => book.BookID == bookId);
        }

        // BORROWER METHODS
        public static void AddBorrower(Borrower borrower)
        {
            Borrowers.Add(borrower);
        }

        public static void RemoveBorrower(string studentId)
        {
            Borrowers.RemoveAll(borrower => borrower.StudentID == studentId);
        }

        public static Borrower FindBorrower(string studentId)
        {
            return Borrowers.Find(borrower => borrower.StudentID == studentId);
        }

        // TRANSACTION METHODS
        public static void BorrowBook(string studentId, string bookId, DateTime dueDate)
        {
            var book = FindBook(bookId);
            var borrower = FindBorrower(studentId);

            if (book != null && borrower != null)
            {
                book.Status = "Borrowed";
                borrower.BorrowedBooksCount++;

                var transaction = new Transaction
                {
                    TransactionID = "TXN-" + (Transactions.Count + 1).ToString("D3"),
                    StudentID = studentId,
                    BookID = bookId,
                    BorrowDate = DateTime.Now,
                    DueDate = dueDate,
                    Status = "Active"
                };

                Transactions.Add(transaction);
            }
        }

        public class Activity
        {
            public string Description { get; set; }
            public DateTime Timestamp { get; set; }
            public string Type { get; set; } // "Book", "Borrower", "Transaction"
        }

        public static class ActivityLogger
        {
            public static List<Activity> Activities = new List<Activity>();
            public static event Action ActivityAdded;

            public static void LogActivity(string description, string type)
            {
                var activity = new Activity
                {
                    Description = description,
                    Timestamp = DateTime.Now,
                    Type = type
                };

                Activities.Insert(0, activity); // Add to beginning for recent first

                // Keep only last 10 activities
                if (Activities.Count > 10)
                    Activities = Activities.Take(10).ToList();

                ActivityAdded?.Invoke();
            }
        }

        public static void InitializeSampleData()
        {
            // Clear existing data
            Books.Clear();
            Borrowers.Clear();
            Transactions.Clear();
            ActivityLogger.Activities.Clear();

            // Sample Books
            var sampleBooks = new List<Book>
    {
        new Book { BookID = "BK-0001", Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", Category = "Computer Science", Status = "Available", DateAdded = DateTime.Now.AddDays(-30) },
        new Book { BookID = "BK-0002", Title = "Database System Concepts", Author = "Abraham Silberschatz", Category = "Computer Science", Status = "Available", DateAdded = DateTime.Now.AddDays(-25) },
        new Book { BookID = "BK-0003", Title = "Physics for Scientists", Author = "Paul A. Tipler", Category = "Physics", Status = "Available", DateAdded = DateTime.Now.AddDays(-20) },
        new Book { BookID = "BK-0004", Title = "Organic Chemistry", Author = "Paula Y. Bruice", Category = "Chemistry", Status = "Available", DateAdded = DateTime.Now.AddDays(-15) },
        new Book { BookID = "BK-0005", Title = "Linear Algebra", Author = "Gilbert Strang", Category = "Mathematics", Status = "Available", DateAdded = DateTime.Now.AddDays(-10) },
        new Book { BookID = "BK-0006", Title = "Digital Marketing", Author = "Philip Kotler", Category = "Marketing", Status = "Available", DateAdded = DateTime.Now.AddDays(-8) },
        new Book { BookID = "BK-0007", Title = "Advanced Database Systems", Author = "Carlo Zaniolo", Category = "Computer Science", Status = "Available", DateAdded = DateTime.Now.AddDays(-5) },
        new Book { BookID = "BK-0008", Title = "Modern Physics", Author = "Kenneth S. Krane", Category = "Physics", Status = "Available", DateAdded = DateTime.Now.AddDays(-3) },
        new Book { BookID = "BK-0009", Title = "Calculus: Early Transcendentals", Author = "James Stewart", Category = "Mathematics", Status = "Available", DateAdded = DateTime.Now.AddDays(-2) },
        new Book { BookID = "BK-0010", Title = "Marketing Management", Author = "Philip Kotler", Category = "Marketing", Status = "Available", DateAdded = DateTime.Now.AddDays(-1) }
    };

            Books.AddRange(sampleBooks);

            // Sample Borrowers
            var sampleBorrowers = new List<Borrower>
    {
        new Borrower { StudentID = "STU-2024-001", Name = "John Smith", ContactNumber = "+639123456789", Email = "john.smith@email.com", Status = "Active" },
        new Borrower { StudentID = "STU-2024-002", Name = "Maria Garcia", ContactNumber = "+639987654321", Email = "maria.garcia@email.com", Status = "Active" },
        new Borrower { StudentID = "STU-2024-003", Name = "David Johnson", ContactNumber = "+639555123456", Email = "david.johnson@email.com", Status = "Active" }
    };

            Borrowers.AddRange(sampleBorrowers);

            // Log initial activities
            ActivityLogger.LogActivity("System initialized with sample data", "System");
            ActivityLogger.LogActivity("10 sample books added to inventory", "Book");
            ActivityLogger.LogActivity("3 sample borrowers registered", "Borrower");
        }











        public static bool AddNewBook(Book book)
        {
            // Validate book
            if (book == null || string.IsNullOrEmpty(book.BookID) || string.IsNullOrEmpty(book.Title))
                return false;

            // Check if book ID already exists
            if (FindBook(book.BookID) != null)
                return false;

            // Add the book
            Books.Add(book);
            return true;
        }

        // Event to notify when books change
        public static event Action BooksChanged;

        // Method to trigger the event
        public static void NotifyBooksChanged()
        {
            BooksChanged?.Invoke();
        }




        public static void ReturnBook(string bookId)
        {
            var transaction = Transactions.Find(t => t.BookID == bookId && t.Status == "Active");
            var book = FindBook(bookId);
            var borrower = FindBorrower(transaction?.StudentID);

            if (transaction != null && book != null && borrower != null)
            {
                book.Status = "Available";
                borrower.BorrowedBooksCount--;
                transaction.ReturnDate = DateTime.Now;
                transaction.Status = "Returned";
            }
        }
    }
}