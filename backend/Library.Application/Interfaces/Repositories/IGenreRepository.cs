using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;

public interface IGenreRepository
{
    Task<Genre> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
}
