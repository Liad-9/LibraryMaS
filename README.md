# LibraryMaS

LibraryMaS is a web-based Library Management System built using ASP.NET Core and MongoDB.  
The application manages books and subscribers, supports book loan and return operations, and connects to a database named **Library**.

---

## Technologies Used

- C#
- ASP.NET Core (Razor Pages)
- MongoDB
- HTML
- CSS
- Dependency Injection
- Routing & Tag Helpers

---

## Database Structure

The application connects to a database called **Library**.

Using MongoDB:
- Collection 1: books_collection
- Collection 2: Subscribers

### Books
Each book includes:
- Id
- Name
- Author
- Genre

### Subscribers
Each subscriber includes:
- Id
- First Name
- Last Name
- List of loaned books (up to 3 books per subscriber)

- A subscriber can loan up to **3 books**
- The same book can be loaned multiple times
- Books can only be deleted if they are not currently on loan

---

## Application Pages

The application includes 4 main Razor Pages:

1️. **Home**
- Navigation to all main sections

2️. **Manage Library**
- Add new books
- Add new subscribers

3️. **Loan / Return Books**
- Loan books to subscribers by Id
- Return books

4️. **Display Information**
- View subscriber details
- View book details
- View books by genre

---

## Technical Implementation

- Forms for user input
- Model Binding
- Tag Handlers
- Routing
- Database registered as a service
- Configuration registered via Dependency Injection
- Optional asynchronous operations
- Custom business logic (loan limits, deletion validation)

---

## Project Purpose

This project demonstrates:
- Backend development with ASP.NET Core
- Database integration with MongoDB
- Razor Pages architecture
- CRUD operations
- Business logic implementation
- Clean service registration and configuration handling

---

## Author

Liad
