using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.RoleFeatures.GetFeaturesByRoleId;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.GetFeaturesByRoleId
{
    public record GetFeaturesByRoleIdRequestViewModel(Role? RoleId);

    public class GetFeaturesByRoleIdRequestValidator : AbstractValidator<GetFeaturesByRoleIdRequestViewModel>
    {
        public GetFeaturesByRoleIdRequestValidator()
        {
        }
    }

    public class GetFeaturesByRoleIdEndPointProfile : Profile
    {
        public GetFeaturesByRoleIdEndPointProfile()
        {
            CreateMap<GetFeaturesByRoleIdRequestViewModel, GetFeaturesByRoleIdQuery>();
        }
    }
}
