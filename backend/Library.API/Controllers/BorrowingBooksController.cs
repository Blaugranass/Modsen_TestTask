
using Library.Application.DTOs.BookDtos;
using Library.Application.DTOs.BorrowingBookDtos;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/borrowing-books")]
    [ApiController]
    public class BorrowingBooksController(IBorrowingService service) : ControllerBase
    {
        [HttpGet("to-user")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<PagedResult<BookResponseDto>>> GetAllBooksToUserAsync(
            [FromQuery] PageParams pageParams,
            CancellationToken cancellationToken)
        {
            var userClaim = HttpContext.User.FindFirst("userId")?.Value;
            var userId = Guid.Parse(userClaim!);

            var books = await service.GetAllBooksToUserAsync(userId, pageParams, cancellationToken);
            return Ok(books);
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<PagedResult<BorrowingBookResponseDto>>> CheckAllBorrowingBooksAsync(
            [FromQuery] PageParams pageParams,
            CancellationToken cancellationToken)
        {
            var books = await service.GetAllBorrowingBooksAsync(pageParams, cancellationToken);

            return Ok(books);
        }

    }
}
