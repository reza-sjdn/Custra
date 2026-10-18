using AutoMapper;
using Custra.Application.Tags.DTOs;
using Custra.Web.Features.Tags.ViewModels;

namespace Custra.Web.AutoMapper;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<TagDto, TagViewModel>();
        CreateMap<TagDto, TagFormViewModel>();

        // Entity <=> Dto

        // Dto => Dto

        // Dto <=> Dto

        // Dto => ViewModel

        // ViewModel => Dto

        // Dto <=> ViewModel

        // ViewModel => ViewModel

        // ViewModel <=> ViewModel

    }
}