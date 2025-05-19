using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.PictureDtos;
using Library.Application.Filters;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateBookAsync([FromBody] CreateBookDto createBook, CancellationToken cancellationToken)
        {
            await bookService.CreateBookAsync(createBook, cancellationToken);
            return NoContent();
        }
        
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBookAsync(Guid id, CancellationToken cancellationToken)
        {
            await bookService.DeleteBookAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<BookResponseDto>> GetBookIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await bookService.GetBookByIdAsync(id, cancellationToken);
            return Ok(book);
        }

        [HttpGet("get-isbn/{isbn}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookResponseDto>> GetBookISBN(string isbn, CancellationToken cancellationToken)
        {
            var book = await bookService.GetBookByISBNAsync(isbn, cancellationToken);
            return Ok(book);
        }
        
        [HttpPatch("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateBookAsync(Guid id, [FromBody] UpdateBookDto dto, CancellationToken cancellationToken)
        {
            dto = dto with { Id = id };
            await bookService.UpdateBookAsync(dto, cancellationToken);
            return NoContent();
        }

        
        [Consumes("multipart/form-data")]
        [HttpPost("add-picture/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddPicture(Guid id, [FromForm] AddPictureDto addPictureDto, CancellationToken cancellationToken)
        {
            await bookService.AddPictureAsync(id, addPictureDto.File, cancellationToken);
            return NoContent();
        }
        

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<PagedResult<BookResponseDto>>> GetBooksAsync(
            [FromQuery] PageParams pageParams,
            [AsParameters, FromQuery] BookFilter filter,
            CancellationToken cancellationToken)
        {
            var books = await bookService.GetBooksAsync(pageParams, filter, cancellationToken);
            return Ok(books);
        }

        [HttpPost("take/{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> TakeBookAsync(Guid id, [FromBody] TakeBookDto dto, CancellationToken cancellationToken)
        {
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            var userId = Guid.Parse(userIdClaim!);
            dto = dto with { BookId = id, UserId = userId };

            await bookService.TakeBookAsync(dto, cancellationToken);
            return NoContent();
        }
    }
}