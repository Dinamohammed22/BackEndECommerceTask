using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.RoleFeatures.UnassignBulkFeaturesFromRole.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.RoleFeatures.UnassignBulkFeatuersToRole
{
    public record UnassignBulkFeatuersToRoleRequestViewModel(Role RoleId, IEnumerable<Feature> Features);
    public class UnassignBulkFeatuersToRoleRequestValidator : AbstractValidator<UnassignBulkFeatuersToRoleRequestViewModel>
    {
        public UnassignBulkFeatuersToRoleRequestValidator()
        {
        }
    }
    public class UnassignBulkFeatuersToRoleRequestProfile : Profile
    {
        public UnassignBulkFeatuersToRoleRequestProfile()
        {
            CreateMap<UnassignBulkFeatuersToRoleRequestViewModel, UnassignBulkFeaturesFromRoleCommand>();
        }
    }
}
