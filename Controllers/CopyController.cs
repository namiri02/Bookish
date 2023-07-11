using Bookish.Models.Book;
using Bookish.Models.Copy;
using Bookish.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;

public class CopyController : Controller
{
    private readonly ILogger<CopyController> _logger;

    public CopyController(ILogger<CopyController> logger)
    {
        _logger = logger;
    }

    public IActionResult Copy(string isbn)
    {
        var bookInfo = BookService.GetBook(isbn);
        
        return View(new CopyStatusListModel() 
        {
            Title = bookInfo.Title,
            Author = bookInfo.Author,
            ListOfCopies = CopyService.GetCopiesWithStatus(isbn)
        });
    }
    
    public IActionResult CopyDelete(string isbn, int copyId)
    {
        CopyService.DeleteCopy(isbn, copyId);
        
        if (CopyService.NumberOfCopies(isbn) == 0)
        {
            BookService.DeleteBook(isbn);
            
            return View("../Home/Catalogue", new BookViewModel()
            {
                ListOfBooks = BookService.GetBooks()
            });
        }

        var book = BookService.GetBook(isbn);
        return View("../Copy/Copy", new CopyStatusListModel() 
        {
            Title = book.Title,
            Author = book.Author,
            ListOfCopies = CopyService.GetCopiesWithStatus(isbn)
        });

    }
    
    public IActionResult AddCopy(string isbn)
    {
        CopyService.AddCopy(isbn);

        var book = BookService.GetBook(isbn);

        return View("../Copy/Copy", new CopyStatusListModel() 
        {
            Title = book.Title,
            Author = book.Author,
            ListOfCopies = CopyService.GetCopiesWithStatus(isbn)
        });
    }
}