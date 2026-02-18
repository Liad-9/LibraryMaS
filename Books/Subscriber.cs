using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplicationTargil2.Books
{
    [BsonIgnoreExtraElements]
    public class Subscriber
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // adjust the mongo id type  
        public string Id { get; set; }

        public string subscriberID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Book> booksOnLoan { get; set; } = new List<Book>(); // Initialize list of books on loan
    }
}