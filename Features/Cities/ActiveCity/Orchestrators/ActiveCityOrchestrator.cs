using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.ActiveCity.Commands;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Cities.ActiveCity.Orchestrators
{
    public record ActiveCityOrchestrator(string ID) : IRequestBase<bool>;
    public class ActiveCityOrchestratorHandler : RequestHandlerBase<City, ActiveCityOrchestrator, bool>
    {
        public ActiveCityOrchestratorHandler(RequestHandlerBaseParameters<City> requestParameters) : base(requestParameters)
        {
        }

        public async  override Task<RequestResult<bool>>Handle(ActiveCityOrchestrator request, CancellationToken cancellationToken)
        {
            var IsActivated = await _mediator.Send(request.MapOne<ActiveCityCommand>());
            if (IsActivated.IsSuccess)
            {
                var GovernorateId = await _repository.Get(c=>c.ID==request.ID).Select(c=>c.GovernorateId).FirstOrDefaultAsync();

                var IsActiveGovernorate = await _mediator.Send(new CheckGovernorateActivationQuery(GovernorateId));
                if (!IsActiveGovernorate.Data)
                {
                    
                        await _mediator.Send(new ActiveGovernorateCommand(GovernorateId));
                    
                }
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }
            var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

            return await Task.FromResult(result);
        }
    }
}
