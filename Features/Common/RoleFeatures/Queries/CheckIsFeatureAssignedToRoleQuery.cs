using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.RoleFeatures.DTOs;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.Common.RoleFeatures.Queries
{
    public record CheckIsFeatureAssignedToRoleQuery(int FeatureID,int RoleID) : IRequestBase<bool>;
    public class CheckIsFeatureAssignedToRoleQueryHandler : RequestHandlerBase<RoleFeature, CheckIsFeatureAssignedToRoleQuery, bool>
    {
        public CheckIsFeatureAssignedToRoleQueryHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(CheckIsFeatureAssignedToRoleQuery request, CancellationToken cancellationToken)
        {
            bool check = _repository.Any(f => ((int)f.Features) == request.FeatureID && ((int)f.RoleId) == request.RoleID);

            if (check)
            {
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Failure();
        }
    }
}
