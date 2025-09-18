using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Models.Cities;

namespace KOG.ECommerce.Features.Cities.BulkDeleteCity.Orchestrator
{
    public record BulkDeleteCityOrchestrator(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeleteCityOrchestratorHandler : RequestHandlerBase<City, BulkDeleteCityOrchestrator, bool>
    {
        public BulkDeleteCityOrchestratorHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteCityOrchestrator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeleteCityCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
