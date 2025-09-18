using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.ActiveCity.Orchestrators;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.BulkActivateCity.Orchestrator
{
    public record BulkActivateCityOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkActivateCityOrchestratorHandler : RequestHandlerBase<City, BulkActivateCityOrchestrator, bool>
    {
        public BulkActivateCityOrchestratorHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateCityOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new ActiveCityOrchestrator(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
