using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.DeactiveCity.Commands;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.BulkDeactivateCity.Orchestrator
{
    public record BulkDeactivateCityOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeactivateCityOrchestratorHandler : RequestHandlerBase<City, BulkDeactivateCityOrchestrator, bool>
    {
        public BulkDeactivateCityOrchestratorHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateCityOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeactiveCityCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
