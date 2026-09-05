using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.MappingProfiles;

public class DeveloperProfile : Profile
{
    public DeveloperProfile()
    {
        CreateMap<Developer, DeveloperDto>();

        CreateMap<CreateDeveloperDto, Developer>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.UserId, options => options.Ignore())
            .ForMember(destination => destination.User, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());

        CreateMap<UpdateDeveloperDto, Developer>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.UserId, options => options.Ignore())
            .ForMember(destination => destination.User, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());
    }
}
