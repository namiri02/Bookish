using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public IActionResult BookForm()
    {
        return View();
    }
  
    [HttpPost]
    public IActionResult SubmitBookForm(string title, string author)
    {
        using (var context = new LibraryContext())
        {
            var bk = new Book()
            {
                Title = title,
                Author = author
            };
            
            context.Book.Add(bk);
            
            context.SaveChanges();

            var books = context.Book;

            return View("../Home/Catalogue", new BookViewModel() 
            { 
                ListOfBooks = books.ToList()
            });
        }
    }
    
    [HttpGet]
    public IActionResult BookEdit(int bookId, string title, string author)
    {
        
        return View(new Book()
        {
            BookId = bookId,
            Title = title,
            Author = author
        });
    }
    
    [HttpPost]
    public IActionResult SubmitBookEdit(int bookId, string title, string author)
    {
        var context = new LibraryContext();

        var member = context.Book.First(m => m.BookId == bookId);

        member.Title = title;
        member.Author = author;

        context.SaveChanges();
            
        var books = context.Book;

        return View("../Home/Catalogue", new BookViewModel() 
        { 
            ListOfBooks = books.ToList()
        });
    }
    
    public IActionResult BookDelete(int bookId)
    {
        var context = new LibraryContext();

        var book = context.Book.First(m => m.BookId == bookId);
        context.Remove(book);

        context.SaveChanges();
            
        var books = context.Book;

        return View("../Home/Catalogue", new BookViewModel() 
        { 
            ListOfBooks = books.ToList()
        });
    }
}