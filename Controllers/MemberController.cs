using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Models.Member;

namespace Bookish.Controllers;

public class MemberController : Controller
{
    private readonly ILogger<MemberController> _logger;

    public MemberController(ILogger<MemberController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult MemberForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitMemberForm(string name, string email)
    {
        var context = new LibraryContext();
        var std = new Member()
            {
                Name = name,
                EmailAddress = email
            };
        context.Member.Add(std);

        context.SaveChanges();
            
        var members = context.Member;

        return View("../Home/Members", new MemberViewModel() 
            { 
                ListOfMembers = members.ToList()
            });
    }
    
    [HttpGet]
    public IActionResult MemberEdit(int memberId, string name, string email)
    {
        
        return View(new Member()
        {
            MemberId = memberId,
            Name = name,
            EmailAddress = email
        });
    }

    [HttpPost]
    public IActionResult SubmitMemberEdit(int memberId, string name, string email)
    {
        var context = new LibraryContext();

        var member = context.Member.First(m => m.MemberId == memberId);

        member.Name = name;
        member.EmailAddress = email;

        context.SaveChanges();
            
        var members = context.Member;

        return View("../Home/Members", new MemberViewModel() 
        { 
            ListOfMembers = members.ToList()
        });
    }
    
    public IActionResult MemberDelete(int memberId)
    {
        var context = new LibraryContext();

        var member = context.Member.First(m => m.MemberId == memberId);
        context.Remove(member);

        context.SaveChanges();
            
        var members = context.Member;

        return View("../Home/Members", new MemberViewModel() 
        { 
            ListOfMembers = members.ToList()
        });
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}