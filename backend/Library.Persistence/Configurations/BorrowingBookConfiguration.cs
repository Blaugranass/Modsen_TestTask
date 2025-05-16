using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class BorrowingBookConfiguration : IEntityTypeConfiguration<BorrowingBook>
{
    public void Configure(EntityTypeBuilder<BorrowingBook> builder)
    {
        builder.ToTable("BorrowingBooks");

        builder.HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.User)
            .WithMany(u => u.BorrowingBooks)
            .HasForeignKey(b => b.UserId);
    }
}