using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        
        builder.HasIndex(b => b.ISBN)
            .IsUnique();
            
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        builder.HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.BookGenreId);
    }
}
