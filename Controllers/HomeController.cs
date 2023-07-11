using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Models.Book;
using Bookish.Models.Loan;
using Bookish.Models.Member;
using Bookish.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Catalogue()
    {
        var context = new LibraryContext();
        var books = context.Book;

        return View(new BookViewModel()
        {
            ListOfBooks = books.ToList()
        });
    }

    public IActionResult Members()
    {
        var context = new LibraryContext();
        var members = context.Member;

        return View(new MemberViewModel()
            {
                ListOfMembers = members.ToList()
            });
}
    
    public IActionResult Loans()
    {
        return View();
    }

    public IActionResult ReturnForm(string isbn, int copyId)
    {
        LoanService.ReturnCopy(isbn, copyId);
        return View("Returns");
    }
    
    public IActionResult Returns()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
