using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.MapFrom(_ => DateTime.UtcNow))
            .ForMember(destination => destination.Developers, options => options.Ignore());

        CreateMap<ApplicationUser, UserDto>()
            .ForMember(destination => destination.CreatedAt,
                options => options.MapFrom(source => source.CreatedAt ?? DateTime.UtcNow))
            .ForMember(destination => destination.Roles, options => options.Ignore());
    }
}
