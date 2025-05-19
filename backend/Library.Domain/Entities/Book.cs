namespace Library.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }

    public required string ISBN { get; set; }

    public required string Name { get; set; }

    public required Genre Genre { get; set; }

    public Guid BookGenreId { get; set; }

    public required string Description { get; set; }

    public required Author Author { get; set; }
    
    public Guid AuthorId { get; set; }

    public string? ImageURL { get; set; }
}