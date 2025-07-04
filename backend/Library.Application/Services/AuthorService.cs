using AutoMapper;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.BookDtos;
using Library.Application.Exceptions;
using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Services;

public class AuthorService(
    IAuthorRepository authorRepository, 
    IMapper mapper) : IAuthorService
{

    public async Task CreateAuthorAsync(CreateAuthorDto dto, CancellationToken cancellationToken)
    {
        var author = mapper.Map<Author>(dto);
        await authorRepository.CreateAsync(author, cancellationToken);
    }

    public async Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
    {
        await authorRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<AuthorDto> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Author not found. ID : {id}");
            
        return mapper.Map<AuthorDto>(author);
    }

    public async Task<PagedResult<AuthorDto>> GetAuthorsAsync(AuthorFilter filter, PageParams pageParams, CancellationToken cancellationToken)
    {
        var authors = await authorRepository.GetAuthorsAsync(filter, pageParams, cancellationToken)
            ?? throw new NotFoundException("Authors not found");

        return mapper.Map<PagedResult<AuthorDto>>(authors);
    }

    public async Task<PagedResult<BookResponseDto>> GetBooksToAuthorAsync(Guid authorId, PageParams pageParams, CancellationToken cancellationToken)
    {
        var books = await authorRepository.GetAllBooksToAuthorAsync(authorId, pageParams, cancellationToken)
            ?? throw new NotFoundException("Not found books to author");
            
        return mapper.Map<PagedResult<BookResponseDto>>(books);
    }


    public async Task UpdateAuthorAsync(UpdateAuthorDto dto, CancellationToken cancellationToken)
    {
        var author = mapper.Map<Author>(dto);
        await authorRepository.UpdateAsync(author, cancellationToken);
    }
}