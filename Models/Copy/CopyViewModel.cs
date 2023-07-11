namespace Bookish.Models.Copy;

public class CopyViewModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public IList<Copy> ListOfCopies { get; set; }
}