using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationTargil2.Books
{
    [BsonIgnoreExtraElements]
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Map MongoDB _id field to Id property

        public int bookID { get; set; }
        public string bookName { get; set; }
        public string bookAuthors { get; set; }
        public string bookGenre { get; set; }
    }
}
