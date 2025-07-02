using System;
using AutoMapper;
using Library.Application.DTOs.AuthorDtos;
using Library.Application.Pagination;
using Library.Domain.Entities;

namespace Library.Application.Mappers;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDto, Author>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<UpdateAuthorDto, Author>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<AuthorDto, Author>();

        CreateMap<PagedResult<Author>, PagedResult<AuthorDto>>()
                .ForCtorParam("data", opt => opt.MapFrom(src => src.Data))
                .ForCtorParam("countElements", opt => opt.MapFrom(src => src.Count));
    }
}
