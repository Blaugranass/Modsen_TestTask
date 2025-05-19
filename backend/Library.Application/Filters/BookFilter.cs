using System;
using Library.Domain.Entities;

namespace Library.Application.Filters;

public class BookFilter
{
    public string? Name { get; set; }

    public Guid? GenreId { get; set; }

    public Guid? AuthorId { get; set; }
}
