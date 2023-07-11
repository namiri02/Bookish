using Bookish.Models.Book;
using Bookish.Models.Copy;

namespace Bookish.Services;

public static class CopyService
{
    public static Copy GetCopy(string isbn, int copyId)
    {
        using (var context = new LibraryContext())
        {
            return context.Copy.First(c => c.ISBN == isbn && c.CopyId == copyId);
        }
    }
    
    public static IList<Copy> GetCopies(string isbn)
    {
        using (var context = new LibraryContext())
        {
            var copies = context.Copy.Where(c => c.ISBN == isbn);
            return copies.ToList();
        }
    }
    
    public static IList<CopyStatusModel> GetCopiesWithStatus(string isbn)
    {
        using (var context = new LibraryContext())
        {
            var copiesWithStatus = new List<CopyStatusModel>();
            foreach (var copy in context.Copy.Where(c => c.ISBN == isbn))
            {
                copiesWithStatus.Add(new CopyStatusModel()
                {
                    Copy = copy,
                    Available = LoanService.IsAvailable(isbn,copy.CopyId),
                    DateDue = LoanService.GetDueDate(isbn,copy.CopyId)
                });
            };
            return copiesWithStatus;
        }
    }

    public static int GetNextCopyId(string isbn)
    {
        using (var context = new LibraryContext())
        {
            if (BookService.GetBook(isbn) == null)
            {
                return 0;
            }
            var currentHighestCopyId = context.Copy.Where(c => c.ISBN == isbn).Max(c => c.CopyId);
            return ++currentHighestCopyId;
        }
    }
    
    public static void AddCopy(string isbn)
    {
        using (var context = new LibraryContext())
        {
            
            var copy = new Copy()
            {
                CopyId = CopyService.GetNextCopyId(isbn),
                ISBN = isbn
            };
            context.Copy.Add(copy);

            context.SaveChanges();
        }
    }
    
    public static int NumberOfCopies(string isbn)
    {
        using (var context = new LibraryContext())
        {
            return context.Copy.Count(m => m.ISBN == isbn);
        }
    }
    
    public static void DeleteCopy(string isbn, int copyId)
    {
        using (var context = new LibraryContext())
        {
            var copy = CopyService.GetCopy(isbn, copyId);
            context.Remove(copy);

            context.SaveChanges();
        }
    }
}