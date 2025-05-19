using AutoMapper;
using Library.Application.DTOs.BorrowingBookDtos;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Mappers;

public class BorrowingBookProfile : Profile
{
    public BorrowingBookProfile()
    {
        CreateMap<PagedResult<BorrowingBook>, PagedResult<BorrowingBookResponseDto>>()
            .ForCtorParam("data", opt => opt.MapFrom(src => src.Data))
            .ForCtorParam("countElements", opt => opt.MapFrom(src => src.Count));
    }

}
