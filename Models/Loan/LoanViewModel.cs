using Bookish.Models.Copy;

namespace Bookish.Models.Loan;

public class LoanViewModel
{
    public int MemberId { get; set; }
    public string ISBN { get; set; }
    public IList<CopyStatusModel> ListOfCopies { get; set; }
}