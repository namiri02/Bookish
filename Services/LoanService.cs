using System.Diagnostics.SymbolStore;
using Bookish.Models.Loan;
using Microsoft.IdentityModel.Tokens;

namespace Bookish.Services;

public static class LoanService
{
    public static void LoanCopy(int memberId, int copyId, string isbn)
    {
        using (var context = new LibraryContext())
        {
            var loan = new Loan()
            {
                CopyId = copyId,
                ISBN = isbn,
                DateCheckedOut = DateOnly.FromDateTime(DateTime.Today),
                DateDue = DateOnly.FromDateTime(DateTime.Today).AddDays(14),
                DateReturned = null,
                MemberId = memberId
            };
            context.Loan.Add(loan);
            context.SaveChanges();
        }
    }
    
    public static void ReturnCopy(string isbn, int copyId)
    {
        using (var context = new LibraryContext())
        {
            var loan = context.Loan.Where(l => l.ISBN == isbn && l.CopyId == copyId && l.DateReturned == null).ToList();

            if (!loan.IsNullOrEmpty())
            {
                loan[0].DateReturned = DateOnly.FromDateTime(DateTime.Today);
            }
            context.SaveChanges();
        }
    }    
    
    public static bool IsAvailable(string isbn, int copyId)
    {
        using (var context = new LibraryContext())
        {
            return !context.Loan.Any(l => l.ISBN == isbn && l.CopyId == copyId && l.DateReturned == null);
        }
    }

    public static DateOnly? GetDueDate(string isbn, int copyId)
    {
        using (var context = new LibraryContext())
        {
            var copyOnLoan = context.Loan.Where(l => l.ISBN == isbn && l.CopyId == copyId && l.DateReturned == null).ToList();
            
            return copyOnLoan.IsNullOrEmpty() ? null : copyOnLoan[0].DateDue;
        }  
    }
}