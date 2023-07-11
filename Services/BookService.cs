using Bookish.Models.Book;
using Bookish.Models.Copy;

namespace Bookish.Services;

public static class BookService
{
    public static Book? GetBook(string isbn)
    {
        using (var context = new LibraryContext())
        {
            return context.Book.Find(isbn);
        }
    }
    public static IList<Book> GetBooks()
    {
        using (var context = new LibraryContext())
        {
            var books = context.Book;
            return books.ToList();
        }
    }
    
    public static void AddBook(string title, string author, string isbn)
    {
        using (var context = new LibraryContext())
        {
            if (BookService.GetBook(isbn) == null)
            {
                var bk = new Book()
                {
                    ISBN = isbn,
                    Title = title,
                    Author = author
                };

                context.Book.Add(bk);

            }

            var copy = new Copy()
            {
                CopyId = CopyService.GetNextCopyId(isbn),
                ISBN = isbn
            };
            context.Copy.Add(copy);

            context.SaveChanges();
        }
    }

    public static void EditBook(string isbn, string title, string author)
    {
        using (var context = new LibraryContext())
        {
            var book = context.Book.First(m => m.ISBN == isbn);

            book.Title = title;
            book.Author = author;

            context.SaveChanges();
        }
    }
    
    public static void DeleteBook(string isbn)
    {
        using (var context = new LibraryContext())
        {
            var book = context.Book.First(m => m.ISBN == isbn);
            context.Remove(book);

            context.SaveChanges();
        }
    }
}