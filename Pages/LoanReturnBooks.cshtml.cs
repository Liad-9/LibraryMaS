using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Linq;
using WebApplicationTargil2.Books;

namespace WebApplicationTargil2.Pages
{
    public class LoanReturnBooksModel : PageModel
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly IMongoCollection<Subscriber> _subscribersCollection;

        public LoanReturnBooksModel(MongoConfig mongoConfig)
        {
            _booksCollection = mongoConfig.books_collection;
            _subscribersCollection = mongoConfig.subscribers_collection;
        }

        [BindProperty]
        public string LoanSubscriberId { get; set; }

        [BindProperty]
        public int LoanBookId { get; set; }

        [BindProperty]
        public string ReturnSubscriberId { get; set; }

        [BindProperty]
        public int ReturnBookId { get; set; }

        public string LoanMessage { get; set; }
        public string ReturnMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostLoan()
        {
            if (string.IsNullOrEmpty(LoanSubscriberId))
            {
                LoanMessage = "All fields are required. Please provide valid Book ID and Subscriber ID greater than or equal to zero.";
                return Page();
            }

            var subscriber = _subscribersCollection.Find(s => s.subscriberID == LoanSubscriberId).FirstOrDefault();
            if (subscriber == null)
            {
                LoanMessage = $"Subscriber with ID {LoanSubscriberId} not found.";
                return Page();
            }

            var book = _booksCollection.Find(b => b.bookID == LoanBookId).FirstOrDefault();
            if (book == null)
            {
                LoanMessage = $"Book with ID {LoanBookId} not found.";
                return Page();
            }

            if (subscriber.booksOnLoan.Count >= 3)
            {
                LoanMessage = $"Subscriber with ID {LoanSubscriberId} has a maximum loan of 3 books.";
                return Page();
            }

            subscriber.booksOnLoan.Add(book);
            _subscribersCollection.ReplaceOne(s => s.subscriberID == LoanSubscriberId, subscriber);

            LoanMessage = $"Book '{book.bookName}' loaned to Subscriber '{subscriber.firstName} {subscriber.lastName}'.";

            return Page();
        }

        public IActionResult OnPostReturn()
        {
            if (string.IsNullOrEmpty(ReturnSubscriberId))
            {
                ReturnMessage = "All fields are required. Please provide valid Book ID and Subscriber ID greater than or equal to zero.";
                return Page();
            }

            var subscriber = _subscribersCollection.Find(s => s.subscriberID == ReturnSubscriberId).FirstOrDefault();
            if (subscriber == null)
            {
                ReturnMessage = $"Subscriber with ID {ReturnSubscriberId} not found.";
                return Page();
            }

            var bookToRemove = subscriber.booksOnLoan.FirstOrDefault(b => b.bookID == ReturnBookId);
            if (bookToRemove == null)
            {
                ReturnMessage = $"Book with ID {ReturnBookId} not found in Subscriber's loaned books.";
                return Page();
            }

            subscriber.booksOnLoan.Remove(bookToRemove);
            _subscribersCollection.ReplaceOne(s => s.subscriberID == ReturnSubscriberId, subscriber);

            ReturnMessage = $"Book '{bookToRemove.bookName}' returned by Subscriber '{subscriber.firstName} {subscriber.lastName}'.";

            return Page();
        }
    }
}
