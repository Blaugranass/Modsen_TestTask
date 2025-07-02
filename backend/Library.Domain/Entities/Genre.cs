namespace Library.Domain.Entities;

public class Genre : BaseEntity 
{
    public required string Name { get; set; }

    public ICollection<Book> Books { get; set; } = [];
}
