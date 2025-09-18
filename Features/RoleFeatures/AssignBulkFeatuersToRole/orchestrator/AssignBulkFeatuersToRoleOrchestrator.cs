using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.RoleFeatures.AssignFeaturesToRole.Commands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.RoleFeatures.AssignBulkFeaturesToRole.Commands
{
    public record AssignBulkFeatuersToRoleOrchestrator(Role RoleId, IEnumerable<Feature> Features) : IRequestBase<bool>;

    public class AssignBulkFeatuersToRoleOrchestratorHandler : RequestHandlerBase<RoleFeature, AssignBulkFeatuersToRoleOrchestrator, bool>
    {
        public AssignBulkFeatuersToRoleOrchestratorHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) 
            : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(AssignBulkFeatuersToRoleOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var feature in request.Features)
            {
                var result = await _mediator.Send(new AssignFeaturesToRoleCommand(request.RoleId,feature));
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
