using System;
using System.Collections.Generic;

namespace Library_Management.Models.Data;

public partial class Book
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Isbn { get; set; }

    public string? Description { get; set; }

    public string? Genre { get; set; }

    public DateTime? PublishedDate { get; set; }

    public bool? IsArchived { get; set; }

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
}
