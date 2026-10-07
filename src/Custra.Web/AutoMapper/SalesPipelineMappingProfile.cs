using AutoMapper;
using Custra.Application.Opportunities.DTOs;
using Custra.Application.SalesPipelines.DTOs;
using Custra.Application.SalesPipelineStages.DTOs;
using Custra.Web.Features.Opportunities.ViewModels;
using Custra.Web.Features.SalesPipelines.ViewModels;

namespace Custra.Web.AutoMapper;

public class SalesPipelineMappingProfile : Profile
{
    public SalesPipelineMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<SalesPipelineDto, SalesPipelineViewModel>();
        CreateMap<SalesPipelineStageDto, SalesPipelineStageViewModel>();

        CreateMap<SalesPipelineDto, EditSalesPipelineViewModel>();
        CreateMap<SalesPipelineStageDto, EditSalesPipelineStageViewModel>();

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