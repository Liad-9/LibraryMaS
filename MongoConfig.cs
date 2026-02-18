using MongoDB.Driver;
using WebApplicationTargil2.Books;

namespace WebApplicationTargil2
{
    public class MongoConfig
    {
        private readonly IMongoDatabase _database;

        public MongoConfig()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Library"); // Connect to the Library database
        }

        public IMongoCollection<Book> books_collection => _database.GetCollection<Book>("books_collection");
        public IMongoCollection<Subscriber> subscribers_collection => _database.GetCollection<Subscriber>("Subscribers");
    }
}

