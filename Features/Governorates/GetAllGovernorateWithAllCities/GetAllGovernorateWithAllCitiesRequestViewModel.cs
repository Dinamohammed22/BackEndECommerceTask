using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.GetListGovernorate;

namespace KOG.ECommerce.Features.Governorates.GetAllGovernorateWithAllCities
{
    public record GetAllGovernorateWithAllCitiesRequestViewModel(string? Name=null,int pageIndex = 1, int pageSize = 100);
    public class GetAllGovernorateWithAllCitiesEndPointRequestValidator : AbstractValidator<GetAllGovernorateWithAllCitiesRequestViewModel>
    {
        public GetAllGovernorateWithAllCitiesEndPointRequestValidator()
        {
            
        }
    }

    public class GetAllGovernorateWithAllCitiesRequestProfile : Profile
    {
        public GetAllGovernorateWithAllCitiesRequestProfile()
        {
            CreateMap<GetAllGovernorateWithAllCitiesRequestViewModel, GetAllGovernorateWithAllCitiesQuery>();
        }
    }
}
