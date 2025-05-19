using System;
using AutoMapper;
using Library.Application.DTOs.BookDtos;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.BookGenreId, opt => opt.MapFrom(src => src.GenreId));

        CreateMap<UpdateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<PagedResult<Book>, PagedResult<BookResponseDto>>()
            .ForCtorParam("data", opt => opt.MapFrom(src => src.Data))
            .ForCtorParam("countElements", opt => opt.MapFrom(src => src.Count));
    }
}
