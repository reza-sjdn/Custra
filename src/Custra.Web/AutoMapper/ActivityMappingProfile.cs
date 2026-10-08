using AutoMapper;
using Custra.Application.Activities.DTOs;
using Custra.Web.Features.Activities.ViewModels;

namespace Custra.Web.AutoMapper;

public class ActivityMappingProfile : Profile
{
    public ActivityMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<ActivityDto, ActivityViewModel>();
        CreateMap<ActivityDto, EditActivityViewModel>();

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