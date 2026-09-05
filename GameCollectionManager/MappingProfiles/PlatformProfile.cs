using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.MappingProfiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformDto>();

        CreateMap<CreatePlatformDto, Platform>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());

        CreateMap<UpdatePlatformDto, Platform>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());
    }
}
