using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.RoleFeatures.Queries;

namespace KOG.ECommerce.Features.RoleFeatures.GetAllFeaturesListed
{
    public record GetAllFeaturesListedRequestViewModel(int RoleID, string? FeatureName);
    public class GetAllFeaturesListedRequestValidator : AbstractValidator<GetAllFeaturesListedRequestViewModel>
    {
        public GetAllFeaturesListedRequestValidator()
        {
        }
    }

    public class GetAllFeaturesListedRequestProfile : Profile
    {
        public GetAllFeaturesListedRequestProfile()
        {
            CreateMap<GetAllFeaturesListedRequestViewModel, GetAllFeaturesListedQuery>();
        }
    }
}
