using AutoMapper;
using Custra.Application.Contacts.DTOs;
using Custra.Application.Customers.Queries.GetCustomerById;
using Custra.Web.Features.Contacts.ViewModels;
using Custra.Web.Features.Customers.ViewModels;

namespace Custra.Web.AutoMapper;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        // Entity => Entity

        // Entity <=> Entity

        // Entity => Dto

        // Dto => Entity
        CreateMap<ContactDto, ContactViewModel>();
        CreateMap<ContactDto, EditContactViewModel>();

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
