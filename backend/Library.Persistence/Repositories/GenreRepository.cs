using System;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class GenreRepository(LibraryDbContext dbContext) : IGenreRepository
{
    public async Task<Genre> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Genres
            .AsNoTracking()
            .Where(g => g.Id == Id)
            .FirstAsync(cancellationToken);
    }
}
