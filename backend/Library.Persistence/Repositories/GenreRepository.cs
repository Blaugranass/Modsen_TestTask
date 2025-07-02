using System;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Repositories;

public class GenreRepository(LibraryDbContext dbContext) : BaseRepository<Genre>(dbContext), IGenreRepository
{
    
}