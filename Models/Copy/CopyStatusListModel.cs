namespace Bookish.Models.Copy;

public class CopyStatusListModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public IList<CopyStatusModel> ListOfCopies { get; set; }
}