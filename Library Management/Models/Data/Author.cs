using System;
using System.Collections.Generic;

namespace Library_Management.Models.Data;

public partial class Author
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Biography { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool? IsArchived { get; set; }
}
