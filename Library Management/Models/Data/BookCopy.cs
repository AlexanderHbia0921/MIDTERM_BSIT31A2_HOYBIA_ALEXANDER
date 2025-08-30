using System;
using System.Collections.Generic;

namespace Library_Management.Models.Data;

public partial class BookCopy
{
    public Guid Id { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Condition { get; set; }

    public string? Source { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? PulloutDate { get; set; }

    public string? PulloutReason { get; set; }

    public Guid? BookId { get; set; }

    public virtual Book? Book { get; set; }
}
