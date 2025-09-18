using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.BulkActivateGovernorate.Orchestrator
{
    public record BulkActivateGovernorateOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkActivateGovernorateOrchestratorHandler : RequestHandlerBase<Governorate, BulkActivateGovernorateOrchestrator, bool>
    {
        public BulkActivateGovernorateOrchestratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateGovernorateOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new ActiveGovernorateCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
