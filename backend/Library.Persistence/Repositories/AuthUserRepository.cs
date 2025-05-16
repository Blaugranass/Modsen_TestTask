using System;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class AuthUserRepository(LibraryDbContext dbContext) : IAuthUserRepository
{
      public async Task CreateAsync(AuthUser user, CancellationToken cancellationToken = default)
    {
        await dbContext.AuthUsers
            .AddAsync(user,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistWithMail(string mail, CancellationToken cancellationToken = default)
    {
        return await dbContext.AuthUsers
            .AsNoTracking()
            .AnyAsync(u => u.Mail == mail, cancellationToken);
    }

    public async Task<AuthUser> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.AuthUsers
            .AsNoTracking()
            .Where(u => u.Id == id)
            .FirstAsync(cancellationToken);
    }

    public async Task<AuthUser> GetByMailAsync(string mail, CancellationToken cancellationToken = default)
    {
        return await dbContext.AuthUsers
            .AsNoTracking()
            .Where(u => u.Mail == mail)
            .FirstAsync(cancellationToken);
    }
}