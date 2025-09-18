using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Features.Clients.GetClientById;
using KOG.ECommerce.Features.Common.Clients.DTOs;

namespace KOG.ECommerce.Features.Cities.CreateCity
{
      
    public record CreateCityResponseViewModel(string ID);
    public class CreateCityResponseProfile : Profile
    {
        public CreateCityResponseProfile()
        {
            CreateMap<CreateCityCommand, CreateCityResponseViewModel>();
        }
    }
}
