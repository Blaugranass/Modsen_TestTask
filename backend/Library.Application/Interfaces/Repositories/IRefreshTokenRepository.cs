using System;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetByTokenAsync(string token);

    Task UpdateAsync(RefreshToken token);

    Task CreateAsync(RefreshToken token);
}
