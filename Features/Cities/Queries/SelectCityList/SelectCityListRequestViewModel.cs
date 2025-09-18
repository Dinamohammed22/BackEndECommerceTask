using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.GetDropdownListGovernorate;

namespace KOG.ECommerce.Features.Cities.Queries.SelectCityList
{
    public record SelectCityListRequestViewModel(string? GovernorateId);
    public class SelectCityListRequestValidator : AbstractValidator<SelectCityListRequestViewModel>
    {
        public SelectCityListRequestValidator(){}

    }
    public class SelectCityListRequestProfile : Profile
    {
        public SelectCityListRequestProfile()
        {
            CreateMap<SelectCityListRequestViewModel, SelectCityListQuery>();
        }
    }

}
