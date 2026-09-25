using AutoMapper;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Web.Features.Customers.ViewModels;

namespace Custra.Web.AutoMapper;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<CustomerDto, CustomerViewModel>().ReverseMap();
        CreateMap<CustomerDto, EditCustomerViewModel>().ReverseMap();

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
