using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Orchestrators;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.BulkDeleteGovernorate.Orchestrator
{
    public record BulkDeleteGovernorateOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeleteGovernorateOrchestratorHandler : RequestHandlerBase<Governorate, BulkDeleteGovernorateOrchestrator, bool>
    {
        public BulkDeleteGovernorateOrchestratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteGovernorateOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeleteGovernorateOrchestrator(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
