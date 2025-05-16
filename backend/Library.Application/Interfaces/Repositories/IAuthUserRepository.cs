using System;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthUserRepository
{
    Task CreateAsync(AuthUser user, CancellationToken cancellationToken = default);

    Task<bool> ExistWithMail(string mail, CancellationToken cancellationToken = default);

    Task<AuthUser> GetByMailAsync(string mail, CancellationToken cancellationToken = default);

    Task<AuthUser> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
