using AutoMapper;
using Custra.Application.Leads.DTOs;
using Custra.Web.Features.Leads.ViewModels;

namespace Custra.Web.AutoMapper;

public class LeadMappingProfile : Profile
{
    public LeadMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<LeadDto, LeadViewModel>();
        CreateMap<LeadDto, EditLeadViewModel>();

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