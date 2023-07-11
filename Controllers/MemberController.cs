using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Bookish.Models.Member;
using Bookish.Services;

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
        MemberService.AddMember(name, email);
        
        return View("../Home/Members", new MemberViewModel() 
            { 
                ListOfMembers = MemberService.GetMembers()
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
        MemberService.EditMember(memberId, name, email);

        return View("../Home/Members", new MemberViewModel() 
        { 
            ListOfMembers = MemberService.GetMembers()
        });
    }
    
    public IActionResult MemberDelete(int memberId)
    {
        MemberService.DeleteMember(memberId);

        return View("../Home/Members", new MemberViewModel() 
        { 
            ListOfMembers = MemberService.GetMembers()
        });
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}