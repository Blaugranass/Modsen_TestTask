using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.BookDtos;
using Library.Application.Filters;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<Author> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken);

    Task CreateAuthorAsync(CreateAuthorDto dto, CancellationToken cancellationToken);

    Task UpdateAuthorAsync(UpdateAuthorDto dto, CancellationToken cancellationToken);

    Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken);

    Task<PagedResult<BookResponseDto>> GetBooksToAuthorAsync(Guid authorId, PageParams pageParams,CancellationToken cancellationToken);

    Task<PagedResult<AuthorDto>> GetAuthorsAsync(AuthorFilter filter, PageParams pageParams, CancellationToken cancellationToken);
}
