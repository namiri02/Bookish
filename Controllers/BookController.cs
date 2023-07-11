using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Models.Book;
using Bookish.Models.Copy;
using Bookish.Services;
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
    public IActionResult SubmitBookForm(string title, string author, string isbn)
    {
        BookService.AddBook(title, author, isbn);
        
        return View("../Home/Catalogue", new BookViewModel() 
            { 
                ListOfBooks = BookService.GetBooks()
            });
    }
    
    [HttpGet]
    public IActionResult BookEdit(string isbn, string title, string author)
    {
        return View(new Book()
        {
            ISBN = isbn,
            Title = title,
            Author = author
        });
    }
    
    [HttpPost]
    public IActionResult SubmitBookEdit(string isbn, string title, string author)
    {
        BookService.EditBook(isbn, title, author);

        return View("../Home/Catalogue", new BookViewModel() 
        { 
            ListOfBooks = BookService.GetBooks()
        });
    }
    
    public IActionResult BookDelete(string isbn)
    {
        BookService.DeleteBook(isbn);

        return View("../Home/Catalogue", new BookViewModel() 
        { 
            ListOfBooks = BookService.GetBooks()
        });
    }
}