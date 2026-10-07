using AutoMapper;
using Custra.Application.Contacts.DTOs;
using Custra.Application.Opportunities.DTOs;
using Custra.Web.Features.Contacts.ViewModels;
using Custra.Web.Features.Opportunities.ViewModels;

namespace Custra.Web.AutoMapper;

public class OpportunityMappingProfile : Profile
{
    public OpportunityMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<OpportunityDto, OpportunityViewModel>();
        CreateMap<OpportunityDto, EditOpportunityViewModel>();

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