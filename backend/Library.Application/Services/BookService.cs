using AutoMapper;
using Library.Application.DTOs.BookDtos;
using Library.Application.Exceptions;
using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Application.Services;

public class BookService(
    IBookRepository bookRepository, 
    IBorrowingBookRepository borrowingBookRepository, 
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository,
    IMapper mapper,
    IWebHostEnvironment env
    ) : IBookService
{

    private readonly string _imageStoragePath = Path.Combine(env.WebRootPath, "images");

    public async Task AddPictureAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException($"Book not found. ID: {id}");

        Directory.CreateDirectory(_imageStoragePath);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(_imageStoragePath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        book.ImageURL = $"/images/{uniqueFileName}";

        await bookRepository.UpdateAsync(book, cancellationToken);
    }

    public async Task CreateBookAsync(CreateBookDto dto, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(dto.AuthorId, cancellationToken)
            ?? throw new NotFoundException($"Author not found. ID: {dto.AuthorId}");

        var genre = await genreRepository.GetByIdAsync(dto.GenreId, cancellationToken)
            ?? throw new NotFoundException($"Genre not found. ID {dto.GenreId}");

        var book = mapper.Map<Book>(dto);
        book.Author = author;
        book.Genre = genre;

        await bookRepository.CreateAsync(book, cancellationToken);
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken)
    {
        await bookRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<PagedResult<BookResponseDto>> GetBooksAsync(
        PageParams pageParams, 
        BookFilter filter, 
        CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAsync(pageParams, filter, cancellationToken)
            ?? throw new NotFoundException("Books not found");

        var bookDto = mapper.Map<PagedResult<BookResponseDto>>(books);

        return bookDto;
    }

    public async Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
    {  
        return await bookRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException($"Book not found. ID: {id}");

    }

    public async Task<Book> GetBookByISBNAsync(string isbn, CancellationToken cancellationToken)
    {
        return await bookRepository.GetByISBNAsync(isbn, cancellationToken) 
            ?? throw new NotFoundException($"Book not found. ISBN: {isbn}");
    }

    public async Task UpdateBookAsync(UpdateBookDto dto, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(dto);

        await bookRepository.UpdateAsync(book, cancellationToken);
    }

    public async Task TakeBookAsync(TakeBookDto dto, CancellationToken cancellationToken)
    {
        if(await borrowingBookRepository.IsTakenAsync(dto.BookId, cancellationToken))
            throw new ConflictException($"Book is taken. ID {dto.BookId}");

        var borrowingBook = mapper.Map<BorrowingBook>(dto);

        await borrowingBookRepository.CreateAsync(borrowingBook, cancellationToken);
    }
}