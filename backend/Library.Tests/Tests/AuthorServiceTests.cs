using System.Data.Common;
using AutoMapper;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.GenreDtos;
using Library.Application.Filters;
using Library.Application.Interfaces.Repositories;
using Library.Application.Pagination;
using Library.Application.Services;
using Library.Domain.Entities;
using Moq;
using Xunit;

namespace Library.Tests.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _authorService = new AuthorService(_authorRepositoryMock.Object, _mapperMock.Object);
        }

        public class GetAuthorByIdAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldReturnAuthor_WhenAuthorExists()
            {
                var authorId = Guid.NewGuid();
                var expectedAuthor = new Author{
                    Id = authorId,
                    FirstName = "John",
                    LastName= "Doe",
                    Birthday = DateOnly.FromDateTime(DateTime.Now),
                    Country = "USA" };
                
                _authorRepositoryMock.Setup(x => x.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedAuthor);

                var result = await _authorService.GetAuthorByIdAsync(authorId, CancellationToken.None);

                Assert.Equal(expectedAuthor, result);
            }

            [Fact]
            public async Task ShouldThrowKeyNotFoundException_WhenAuthorNotFound()
            {
                var authorId = Guid.NewGuid();
                _authorRepositoryMock.Setup(x => x.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((Author)null);

                await Assert.ThrowsAsync<KeyNotFoundException>(() => 
                    _authorService.GetAuthorByIdAsync(authorId, CancellationToken.None));
            }
        }

        public class CreateAuthorAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldCreateAuthor_WithValidData()
            {
                var dto = new CreateAuthorDto(
                    "John", 
                    "Doe", 
                    DateOnly.FromDateTime(DateTime.Now), 
                    "USA");

                var author = new Author {
                    Id = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Birthday = dto.Birthday,
                    Country = dto.Country
                };
                
                _mapperMock.Setup(x => x.Map<Author>(dto)).Returns(author);
                _authorRepositoryMock.Setup(x => x.CreateAsync(author, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                await _authorService.CreateAuthorAsync(dto, CancellationToken.None);

                _mapperMock.Verify(x => x.Map<Author>(dto), Times.Once);
                _authorRepositoryMock.Verify(x => x.CreateAsync(author, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class UpdateAuthorAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldUpdateAuthor_WithValidData()
            {
                var authorId = Guid.NewGuid();
                var dto = new UpdateAuthorDto(
                    Id: authorId,
                    FirstName: "UpdatedName",
                    LastName: "UpdatedLastName",
                    Birthday: DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                    Country: "UpdatedCountry");

                var author = new Author{
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Birthday = dto.Birthday,
                    Country = dto.Country
                };
                
                _mapperMock.Setup(x => x.Map<Author>(dto)).Returns(author);
                _authorRepositoryMock.Setup(x => x.UpdateAsync(author, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                await _authorService.UpdateAuthorAsync(dto, CancellationToken.None);

                _mapperMock.Verify(x => x.Map<Author>(dto), Times.Once);
                _authorRepositoryMock.Verify(x => x.UpdateAsync(author, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class DeleteAuthorAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldDeleteAuthor_WhenAuthorExists()
            {
                var authorId = Guid.NewGuid();
                _authorRepositoryMock.Setup(x => x.DeleteAsync(authorId, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                await _authorService.DeleteAuthorAsync(authorId, CancellationToken.None);

                _authorRepositoryMock.Verify(x => x.DeleteAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class GetBooksToAuthorAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldReturnPagedBooks_WhenAuthorExists()
            {
                var authorId = Guid.NewGuid();
                var pageParams = new PageParams();
                
                var author = new Author{
                    Id = authorId,
                    FirstName = "John",
                    LastName = "Doe",
                    Birthday = DateOnly.FromDateTime(DateTime.Now),
                    Country = "USA" };
                
                var genre = new Genre { Id = Guid.NewGuid(), Name = "Fiction" };
                
                var books = new List<Book> 
                { 
                    new Book{
                        Id = Guid.NewGuid(),
                        ISBN = "978-3-16-148410-0",
                        Name = "Test Book",
                        BookGenreId = genre.Id,
                        Genre = genre,
                        Description = "Test Description",
                        AuthorId = author.Id,
                        Author = author,
                        ImageURL = null }
                };
                
                var pagedBooks = new PagedResult<Book>(books.ToArray(), books.Count);
                
                var expectedDtos = new PagedResult<BookResponseDto>(
                    new BookResponseDto[] 
                    { 
                        new BookResponseDto(
                            books[0].Id,
                            books[0].ISBN,
                            books[0].Name,
                            books[0].Description,
                            new AuthorDto(
                                author.Id,
                                author.FirstName,
                                author.LastName,
                                author.Birthday,
                                author.Country),
                            new GenreDto(genre.Id, genre.Name),
                            books[0].ImageURL)
                    }, 
                    books.Count);

                _authorRepositoryMock.Setup(x => x.GetAllBooksToAuthorAsync(
                    authorId, 
                    pageParams, 
                    It.IsAny<CancellationToken>()))
                    .ReturnsAsync(pagedBooks);
                
                _mapperMock.Setup(m => m.Map<PagedResult<BookResponseDto>>(pagedBooks))
                    .Returns(expectedDtos);

                var result = await _authorService.GetBooksToAuthorAsync(
                    authorId, 
                    pageParams, 
                    CancellationToken.None);

                Assert.Equal(expectedDtos, result);
            }

            [Fact]
            public async Task ShouldThrowNullReferenceException_WhenNoBooksFound()
            {
                var authorId = Guid.NewGuid();
                var pageParams = new PageParams();
                
                _authorRepositoryMock.Setup(x => x.GetAllBooksToAuthorAsync(
                    authorId, 
                    It.IsAny<PageParams>(), 
                    It.IsAny<CancellationToken>()))
                    .ReturnsAsync((PagedResult<Book>)null);

                await Assert.ThrowsAsync<NullReferenceException>(() => 
                    _authorService.GetBooksToAuthorAsync(authorId, pageParams, CancellationToken.None));
            }
        }

        public class GetAuthorsAsync : AuthorServiceTests
        {
            [Fact]
            public async Task ShouldReturnPagedAuthorDtos_WithFilters()
            {
                var filter = new AuthorFilter { LastName = "Test" };
                var pageParams = new PageParams { Page = 1, PageSize = 10 };
                
                var authors = new List<Author> 
                { 
                    new Author{
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Test",
                        Birthday = DateOnly.FromDateTime(DateTime.Now),
                        Country = "USA" }
                };
                
                var pagedAuthors = new PagedResult<Author>(authors.ToArray(), authors.Count);
                
                var expectedDtos = new PagedResult<AuthorDto>(
                    new AuthorDto[] 
                    { 
                        new AuthorDto(
                            authors[0].Id,
                            authors[0].FirstName,
                            authors[0].LastName,
                            authors[0].Birthday,
                            authors[0].Country)
                    }, 
                    authors.Count);

                _authorRepositoryMock.Setup(x => x.GetAuthorsAsync(
                    filter, 
                    pageParams, 
                    It.IsAny<CancellationToken>()))
                    .ReturnsAsync(pagedAuthors);
                
                _mapperMock.Setup(m => m.Map<PagedResult<AuthorDto>>(pagedAuthors))
                    .Returns(expectedDtos);

                var result = await _authorService.GetAuthorsAsync(filter, pageParams, CancellationToken.None);

                Assert.Equal(expectedDtos, result);
                _mapperMock.Verify(m => m.Map<PagedResult<AuthorDto>>(pagedAuthors), Times.Once);
            }
        }
    }
}