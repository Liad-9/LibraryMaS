using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTargil2.Books;
using static WebApplicationTargil2.Books.BookManagement;
using MongoDB.Driver;

namespace WebApplicationTargil2.Pages
{
    public class AddBookSubscModel : PageModel
    {
        private readonly BooksManagement _booksManagement;
        private readonly IMongoCollection<Subscriber> _subscribersCollection;

        public AddBookSubscModel(BooksManagement booksManagement, MongoConfig mongoConfig)
        {
            _booksManagement = booksManagement;
            _subscribersCollection = mongoConfig.subscribers_collection;
        }

        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        public Subscriber Subscriber { get; set; }

        public string BookMessage { get; set; }
        public string SubscriberMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPostAddBook()
        {
            // Validate Book fields 
            if (Book == null || string.IsNullOrEmpty(Book.bookName) || string.IsNullOrEmpty(Book.bookAuthors) || Book.bookID <= 0 ||
                string.IsNullOrEmpty(Book.bookGenre))
            {
                BookMessage = "All book fields are required. Please provide valid Book ID greater than or equal to zero, Name, Authors and Genre.";
                return; // Return to the page with the error message
            }

            var existingBook = _booksManagement.GetBookById(Book.bookID);
            if (existingBook != null)
            {
                BookMessage = $"A book with ID {Book.bookID} already exists.";
                return; 
            }

            _booksManagement.AddBook(Book.bookID, Book.bookName, Book.bookAuthors, Book.bookGenre);
            BookMessage = "Book added successfully!";
        }

        public void OnPostAddSubscriber()
        {
            // Validate Subscriber fields 
            if (Subscriber == null || string.IsNullOrEmpty(Subscriber.subscriberID) || string.IsNullOrEmpty(Subscriber.firstName) ||
                string.IsNullOrEmpty(Subscriber.lastName))
            {
                SubscriberMessage = "All subscriber fields are required. Please provide valid Subscriber ID greater than or equal to zero, First Name and Last Name.";
                return; // Return to the page with the error message
            }

            var existingSubscriber = _subscribersCollection.Find(s => s.subscriberID == Subscriber.subscriberID).FirstOrDefault();
            if (existingSubscriber != null)
            {
                SubscriberMessage = $"A subscriber with ID {Subscriber.subscriberID} already exists.";
                return; 
            }

            _booksManagement.AddSubscriber(Subscriber.subscriberID, Subscriber.firstName, Subscriber.lastName);
            SubscriberMessage = "Subscriber added successfully!";
        }
    }
}
