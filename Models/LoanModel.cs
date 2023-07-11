﻿namespace Bookish.Models;

public class Loan
{
    public int CopyId { get; set; }
    public int MemberId { get; set; }
    public DateOnly DateCheckedOut { get; set; }
    public DateOnly DateDue { get; set; }
    public DateOnly DateReturned { get; set; }

}