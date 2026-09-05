using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.MappingProfiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>();

        CreateMap<CreateGameDto, Game>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.DeveloperId, options => options.Ignore())
            .ForMember(destination => destination.Developer, options => options.Ignore())
            .ForMember(destination => destination.Genres, options => options.Ignore())
            .ForMember(destination => destination.Platforms, options => options.Ignore());

        CreateMap<UpdateGameDto, Game>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.CreatedAt, options => options.Ignore())
            .ForMember(destination => destination.DeveloperId, options => options.Ignore())
            .ForMember(destination => destination.Developer, options => options.Ignore())
            .ForMember(destination => destination.Genres, options => options.Ignore())
            .ForMember(destination => destination.Platforms, options => options.Ignore());
    }
}
