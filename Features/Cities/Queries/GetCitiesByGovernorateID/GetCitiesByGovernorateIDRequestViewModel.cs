using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;

namespace KOG.ECommerce.Features.Cities.Queries.GetCitiesByGovernorateID
{
    public record GetCitiesByGovernorateIDRequestViewModel(string GovernorateId);

    public class GetCitiesByGovernorateIDRequestValidator : AbstractValidator<GetCitiesByGovernorateIDRequestViewModel>
    {
        public GetCitiesByGovernorateIDRequestValidator()
        {
            RuleFor(request => request.GovernorateId).NotEmpty();
        }
    }

    public class GetCitiesByGovernorateIDRequestProfile : Profile
    {
        public GetCitiesByGovernorateIDRequestProfile()
        {
            CreateMap<GetCitiesByGovernorateIDRequestViewModel, GetCitiesByGovernorateIDQuery>();
        }
    }
}
