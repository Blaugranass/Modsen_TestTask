using System;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
{
    public void Configure(EntityTypeBuilder<AuthUser> builder)
    {
        builder.ToTable("AuthUsers");

        builder.HasDiscriminator<string>("UserType")
            .HasValue("User")
            .HasValue("Admin");

        builder.Property(u => u.Mail)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(u => u.Mail)
            .IsUnique();
    }
}
