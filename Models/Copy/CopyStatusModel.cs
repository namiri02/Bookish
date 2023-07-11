namespace Bookish.Models.Copy;

public class CopyStatusModel
{
    public Copy Copy { get; set; }
    public bool Available { get; set; }
    public DateOnly? DateDue { get; set; }
    
}