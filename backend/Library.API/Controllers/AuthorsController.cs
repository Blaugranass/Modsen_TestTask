using Library.Application.DTOs.AuthorDtos;
using Library.Application.DTOs.BookDtos;
using Library.Application.Filters;
using Library.Application.Interfaces.Services;
using Library.Application.Pagination;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController(IAuthorService service) : ControllerBase
    {
        [HttpPost("create")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> CreateAuthorAsync(
            [FromBody] CreateAuthorDto dto,
            CancellationToken cancellationToken)
        {
            await service.CreateAuthorAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> DeleteAuthorAsync(Guid id, CancellationToken cancellationToken)
        {
            await service.DeleteAuthorAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPatch("update/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult> UpdateAuthorAsync(
            Guid id,
            [FromBody] UpdateAuthorDto dto,
            CancellationToken cancellationToken)
        {
            dto = dto with { Id = id };
            await service.UpdateAuthorAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpGet("get-to-author/{id}")]
        [Authorize(Policy = "AdminOrUserPolicy")]
        public async Task<ActionResult<PagedResult<BookResponseDto>>> GetBooksToAuthorAsync(
            Guid id,
            [FromQuery] PageParams pageParams,
            CancellationToken cancellationToken)
        {
            var books = await service.GetBooksToAuthorAsync(id, pageParams, cancellationToken);
            return Ok(books);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOrUserPolicy")]
        public async Task<ActionResult<PagedResult<AuthorDto>>> GetAuthorsAsync(
            [AsParameters, FromQuery] AuthorFilter filter,
            [FromQuery] PageParams pageParams,
            CancellationToken cancellationToken)
        {
            var authors = await service.GetAuthorsAsync(filter, pageParams, cancellationToken);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrUserPolicy")]
        public async Task<ActionResult<AuthorDto>> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var author = await service.GetAuthorByIdAsync(id, cancellationToken);
            return Ok(author);
        }

    }
}
