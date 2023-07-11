using Bookish.Models.Loan;
using Bookish.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookish.Controllers;

public class LoanController : Controller
{
    private readonly ILogger<LoanController> _logger;

    public LoanController(ILogger<LoanController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult ShowCopies(string isbn, int memberId)
    {
        return View("../Loan/SelectLoan", new LoanViewModel() 
        {
            MemberId = memberId,
            ISBN = isbn,
            ListOfCopies = CopyService.GetCopiesWithStatus(isbn)
        });
    }

    public IActionResult LoanCopy(string isbn, int copyId, int memberId)
    {
        LoanService.LoanCopy(memberId,copyId,isbn);
        return View("../Home/Loans");
    }
    
}