using AutoMapper;
using GameCollectionManager.DTOs;
using GameCollectionManager.Models;

namespace GameCollectionManager.MappingProfiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();

        CreateMap<CreateGenreDto, Genre>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());

        CreateMap<UpdateGenreDto, Genre>()
            .ForMember(destination => destination.Id, options => options.Ignore())
            .ForMember(destination => destination.Games, options => options.Ignore());
    }
}
