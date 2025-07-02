using System;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthUserRepository : IRepository<AuthUser>
{
    Task<bool> ExistWithMail(string mail, CancellationToken cancellationToken = default);

    Task<AuthUser> GetByMailAsync(string mail, CancellationToken cancellationToken = default);
}
