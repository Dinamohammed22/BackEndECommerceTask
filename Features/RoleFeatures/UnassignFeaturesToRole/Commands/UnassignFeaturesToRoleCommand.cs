using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.RoleFeatures.UnassignFeaturesToRole.Commands
{
    public record UnassignFeaturesToRoleCommand(Role RoleId, Feature Feature) : IRequestBase<bool>;
    public class UnassignFeaturesToRoleCommandHandler : RequestHandlerBase<RoleFeature, UnassignFeaturesToRoleCommand, bool>
    {
        public UnassignFeaturesToRoleCommandHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(UnassignFeaturesToRoleCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(Role), request.RoleId))
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var existingFeatureID = _repository
                .Get(rf => rf.RoleId == request.RoleId && rf.Features == request.Feature).Select(rf => rf.ID).FirstOrDefault();

            if (!existingFeatureID.IsNullOrEmpty())
            {
                _repository.Delete(existingFeatureID);
                _repository.SaveChanges();
            }

            return RequestResult<bool>.Success(true);
        }

    }
}
