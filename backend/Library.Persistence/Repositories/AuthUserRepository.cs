using System;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class AuthUserRepository(LibraryDbContext dbContext) : BaseRepository<AuthUser>(dbContext), IAuthUserRepository
{

    public async Task<bool> ExistWithMail(string mail,
        CancellationToken cancellationToken = default)
    {
        return await ExistAsync(u => u.Mail == mail, cancellationToken);
    }

    public async Task<AuthUser> GetByMailAsync(string mail,
        CancellationToken cancellationToken = default)
    {
        return await FirstAsync(u => u.Mail == mail, cancellationToken);
    }
}