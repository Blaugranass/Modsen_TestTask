namespace Library.Domain.Entities;

public class Author
{
    public Guid Id { get; set;}

    public string FirstName { get; set;} = string.Empty;

    public string LastName { get; set;} = string.Empty;

    public DateOnly Birthday { get; set;} 

    public string Country { get; set;} = string.Empty;

    public ICollection<Book> Books { get; set;} = [];

}