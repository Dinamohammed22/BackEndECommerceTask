using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;

namespace KOG.ECommerce.Features.Cities.Queries.GetByCityNameOrGovernName
{
    public record GetByCityNameOrGovernNameRequestViewModel(string? CityName=null, string? GovernorateId = null, int pageIndex = 1, int pageSize = 100);
   
    public class GetByCityNameOrGovernNameRequestValidator : AbstractValidator<GetByCityNameOrGovernNameRequestViewModel>
    {
        public GetByCityNameOrGovernNameRequestValidator()
        {

        }
    }

    public class GetByCityNameOrGovernNameRequestProfile : Profile
    {
        public GetByCityNameOrGovernNameRequestProfile()
        {
            CreateMap<GetByCityNameOrGovernNameRequestViewModel, GetByCityNameOrGovernNameQuery>();
        }
    }

}
