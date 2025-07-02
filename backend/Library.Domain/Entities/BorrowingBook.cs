namespace Library.Domain.Entities;

public class BorrowingBook : BaseEntity
{
    public required Guid BookId { get; set; }

    public Book Book { get; set; } = null!;

    public DateTime Taken { get; set; }

    public DateTime DueDate { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public bool IsReturned { get; set; } = false;
}