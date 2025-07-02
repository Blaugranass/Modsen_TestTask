namespace Library.Domain.Entities;

public class User : AuthUser
{
    public required string Number { get; set; }
    
    public ICollection<BorrowingBook> BorrowingBooks { get; set; } = [];
}