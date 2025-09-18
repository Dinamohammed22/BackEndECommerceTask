using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.RoleFeatures.AssignAllFeaturesToRole.Commands
{
    public record AssignAllFeaturesToRoleCommand():IRequestBase<bool>;
    public class AssignAllFeaturesToRoleCommandHandler : RequestHandlerBase<RoleFeature, AssignAllFeaturesToRoleCommand, bool>
    {
        public AssignAllFeaturesToRoleCommandHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AssignAllFeaturesToRoleCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(Role), Role.SuperAdmin))
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var allFeatures = Enum.GetValues(typeof(Feature)).Cast<Feature>().ToList();

            var selectableFeatures = EnumHelper.ToSelectableList<Feature>();

            var assignedFeatures = _repository
                .Get(rf => rf.RoleId == Role.SuperAdmin)
                .Select(rf => rf.Features)
                .ToList();
            List<RoleFeature> roleFeatures = new List<RoleFeature>();   
            var newFeatures = allFeatures.Except(assignedFeatures).ToList();

            foreach (var feature in newFeatures)
            {
                var roleFeature = new RoleFeature
                {
                    RoleId = Role.SuperAdmin,
                    Features = feature
                };

                roleFeatures.Add(roleFeature);
            }
            _repository.AddRange(roleFeatures);
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
