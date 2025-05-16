using Library.Application.DTOs.BookDtos;

namespace Library.Application.DTOs.BorrowingBookDtos;

public record class BorrowingBookResponseDto
(
    Guid Id,
    bool IsReturned,
    DateTime Taken,
    DateTime DueDate,
    Guid UserId,
    BookResponseDto Book
);