using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace WebApplicationTargil2.Books
{
    public class BookManagement
    {
        public class BooksManagement
        {
            private readonly IMongoCollection<Book> _booksCollection;
            private readonly IMongoCollection<Subscriber> _subscribersCollection;

            public BooksManagement(MongoConfig mongoConfig)
            {
                _booksCollection = mongoConfig.books_collection;
                _subscribersCollection = mongoConfig.subscribers_collection;

            }

            public List<Book> GetAllBooks()
            {
                return _booksCollection.Find(book => true).ToList();
            }

            public Book GetBookById(int id)
            {
                return _booksCollection.Find(book => book.bookID == id).FirstOrDefault();
            }

            public List<Book> SearchBooksByName(string name)
            {
                var filter = Builders<Book>.Filter.Regex("bookName", new MongoDB.Bson.BsonRegularExpression(name, "i")); // Case-insensitive search
                return _booksCollection.Find(filter).ToList();
            }

            public void AddBook(int bookID, string bookName, string bookAuthors, string bookGenre)
            {
                Console.WriteLine($"Adding book: ID={bookID}, Name={bookName}, Authors={bookAuthors}, Genre={bookGenre}");
                var newBook = new Book

                {
                    bookID = bookID,
                    bookName = bookName,
                    bookAuthors = bookAuthors,
                    bookGenre = bookGenre
                };

                _booksCollection.InsertOne(newBook);
            }

            public void AddSubscriber(string subscriberID, string firstName, string lastName)
            {
                var newSubscriber = new Subscriber
                {
                    subscriberID = subscriberID,
                    firstName = firstName,
                    lastName = lastName
                };

                _subscribersCollection.InsertOne(newSubscriber);
            }
        }
    }
}
