using System;
using System.Runtime.CompilerServices;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class RefreshTokenRepository(LibraryDbContext dbContext) : BaseRepository<RefreshToken>(dbContext), IRefreshTokenRepository
{
    private readonly LibraryDbContext _dbContext = dbContext;
    
    public async Task<RefreshToken> GetByTokenAsync(string token,
        CancellationToken cancellationToken = default)
    {
        return await FirstAsync(r => r.Token == token, cancellationToken);
    }

    public async Task UpdateAsync(RefreshToken token,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.RefreshTokens
            .ExecuteUpdateAsync(pr => pr
            .SetProperty(t => t.IsRevoked, token.IsRevoked),
                cancellationToken);
    }
}
