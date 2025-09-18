using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;

namespace KOG.ECommerce.Features.Cities.Queries.GetCityByID
{
    public record GetCityByIDRequestViewModel(string ID);

    public class GetCityByIDRequestValidator : AbstractValidator<GetCityByIDRequestViewModel>
    {
        public GetCityByIDRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty();
        }
    }

    public class GetCityByIDRequestProfile : Profile
    {
        public GetCityByIDRequestProfile()
        {
            CreateMap<GetCityByIDRequestViewModel, GetCityByIDQuery>();
        }
    }

}
