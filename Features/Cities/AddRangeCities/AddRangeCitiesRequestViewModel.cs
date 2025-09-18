using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.AddRangeCities.Commands;
using KOG.ECommerce.Features.Common.Cities.DTOs;

namespace KOG.ECommerce.Features.Cities.AddRangeCities
{
    public record AddRangeCitiesRequestViewModel(List<CityDTO> cities);
    public class AddRangeCitiesRequestValidator : AbstractValidator<AddRangeCitiesRequestViewModel>
    {
        public AddRangeCitiesRequestValidator()
        {
           
        }
    }
    public class AddRangeCitiesRequestProfile : Profile
    {
        public AddRangeCitiesRequestProfile()
        {
            CreateMap<AddRangeCitiesRequestViewModel, AddRangeCitiesCommand>();

        }
    }
}
