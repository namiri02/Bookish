namespace Bookish.Models.Book;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}