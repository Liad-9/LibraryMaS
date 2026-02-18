using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplicationTargil2.Books;

namespace WebApplicationTargil2.Pages
{
    public class DisplayInformationModel : PageModel
    {
        private readonly MongoConfig _mongoConfig;

        public DisplayInformationModel(MongoConfig mongoConfig)
        {
            _mongoConfig = mongoConfig;
        }

        [BindProperty]
        public string searchBook { get; set; }

        [BindProperty]
        public string searchSubscriber { get; set; }

        [BindProperty]
        public string searchGenre { get; set; }

        public List<Book> Books { get; set; }
        public List<Subscriber> Subscribers { get; set; }
        public List<Book> BooksByGenre { get; set; }

        public string BookMessage { get; set; }
        public string SubscriberMessage { get; set; }
        public string GenreMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPostSearchBook()
        {
            Books = new List<Book>();

            if (!string.IsNullOrEmpty(searchBook))
            {
                var bookCollection = _mongoConfig.books_collection;

                if (int.TryParse(searchBook, out int id))
                {
                    var filter = Builders<Book>.Filter.Eq(b => b.bookID, id);
                    var book = bookCollection.Find(filter).FirstOrDefault();

                    if (book != null)
                    {
                        Books.Add(book);
                    }
                    else
                    {
                        BookMessage = $"No book found with ID {id}.";
                    }
                }
                else
                {
                    var filter = Builders<Book>.Filter.Regex(b => b.bookName, new BsonRegularExpression(searchBook, "i"));
                    Books = bookCollection.Find(filter).ToList();

                    if (!Books.Any())
                    {
                        BookMessage = $"No books found with name containing \"{searchBook}\".";
                    }
                }
            }
            else
            {
                BookMessage = "Please enter a book ID or a name to search.";
            }
        }

        public void OnPostSearchSubscriber()
        {
            Subscribers = new List<Subscriber>();

            if (!string.IsNullOrEmpty(searchSubscriber))
            {
                var subscriberCollection = _mongoConfig.subscribers_collection;

                if (int.TryParse(searchSubscriber, out int id))
                {
                    // Search by subscriberID as string
                    var filter = Builders<Subscriber>.Filter.Eq(s => s.subscriberID, searchSubscriber);
                    Subscribers = subscriberCollection.Find(filter).ToList();

                    if (!Subscribers.Any())
                    {
                        SubscriberMessage = $"No subscriber found with ID \"{searchSubscriber}\".";
                    }
                }
                else
                {
                    // Search by name
                    var filter = Builders<Subscriber>.Filter.Regex(s => s.firstName, new MongoDB.Bson.BsonRegularExpression(searchSubscriber, "i")) |
                                 Builders<Subscriber>.Filter.Regex(s => s.lastName, new MongoDB.Bson.BsonRegularExpression(searchSubscriber, "i"));
                    Subscribers = subscriberCollection.Find(filter).ToList();

                    if (!Subscribers.Any())
                    {
                        SubscriberMessage = $"No subscribers found with name containing \"{searchSubscriber}\".";
                    }
                }
            }
            else
            {
                SubscriberMessage = "Please enter a subscriber ID or a name to search.";
            }
        }


        public void OnPostSearchGenre()
        {
            BooksByGenre = new List<Book>();

            if (!string.IsNullOrEmpty(searchGenre))
            {
                var bookCollection = _mongoConfig.books_collection;

                var filter = Builders<Book>.Filter.Regex(b => b.bookGenre, new BsonRegularExpression(searchGenre, "i"));
                BooksByGenre = bookCollection.Find(filter).ToList();

                if (!BooksByGenre.Any())
                {
                    GenreMessage = $"No books found with genre containing \"{searchGenre}\".";
                }
            }
            else
            {
                GenreMessage = "Please enter a genre to search.";
            }
        }
    }
}
