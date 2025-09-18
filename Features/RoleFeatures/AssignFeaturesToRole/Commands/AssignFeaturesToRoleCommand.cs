using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole.Commands
{
    public record AssignFeaturesToRoleCommand(Role RoleId, Feature Feature):IRequestBase<bool>;
    public class AssignFeaturesToRoleCommandHandler : RequestHandlerBase<RoleFeature, AssignFeaturesToRoleCommand, bool>
    {
        public AssignFeaturesToRoleCommandHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AssignFeaturesToRoleCommand request, CancellationToken cancellationToken)
        {


            if (!Enum.IsDefined(typeof(Role), request.RoleId))
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);


            var existingFeatureID = _repository
                .GetWithDeleted().Where(rf => rf.RoleId == request.RoleId && rf.Features == request.Feature && rf.Deleted == true).Select(rf => rf.ID).FirstOrDefault();

            if (!existingFeatureID.IsNullOrEmpty())
            {
                RoleFeature updatedFeature = new RoleFeature()
                {
                    ID = existingFeatureID,
                    Deleted = false,
                };
               _repository.SaveIncluded(updatedFeature,nameof(updatedFeature.Deleted));

            }
            else
            {
                var existingFeature = _repository
                .Any(rf => rf.RoleId == request.RoleId && rf.Features == request.Feature && rf.Deleted == false);

                if (!existingFeature)
                {
                    var roleFeature = new RoleFeature
                    {
                        RoleId = request.RoleId,
                        Features = request.Feature
                    };

                    _repository.Add(roleFeature);
                  
                }
                
            }
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }

    }

}
