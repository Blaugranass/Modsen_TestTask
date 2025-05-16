using AutoMapper;
using Library.Application.DTOs.UserDtos;
using Library.Domain.Entities;

namespace Library.Application.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
    }
}
