using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Orchestrators;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.BulkDeactivateGovernorate.Orchestrator
{
    public record BulkDeactivateGovernorateOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeactivateGovernorateOrchestratorHandler : RequestHandlerBase<Governorate, BulkDeactivateGovernorateOrchestrator, bool>
    {
        public BulkDeactivateGovernorateOrchestratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateGovernorateOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeactiveGovernorateOrchestrator(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }

}
