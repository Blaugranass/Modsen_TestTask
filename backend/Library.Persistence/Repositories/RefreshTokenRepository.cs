using System;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class RefreshTokenRepository(LibraryDbContext dbContext) : IRefreshTokenRepository
{
    public async Task CreateAsync(RefreshToken token)
    {
        await dbContext.RefreshTokens
            .AddAsync(token);

        await dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        return await dbContext.RefreshTokens
            .AsNoTracking()
            .Where(t => t.Token == token)
            .FirstAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        await dbContext.RefreshTokens
            .ExecuteUpdateAsync(pr => pr
            .SetProperty(t => t.IsRevoked, token.IsRevoked));
        
        await dbContext.SaveChangesAsync();
    }
}
