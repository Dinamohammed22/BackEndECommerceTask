using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.DeactiveCity.Commands;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Orchestrators
{
    public record DeactiveGovernorateOrchestrator(string ID) : IRequestBase<bool>;
    public class DeactiveGovernorateOrchestratorHandler : RequestHandlerBase<Governorate, DeactiveGovernorateOrchestrator, bool>
    {
        public DeactiveGovernorateOrchestratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveGovernorateOrchestrator request, CancellationToken cancellationToken)
        {
            var IsDeactivated = await _mediator.Send(request.MapOne<DeactiveGovernorateCommand>());
            if (IsDeactivated.IsSuccess)
            {
                var citiesIds = await _mediator.Send(request.MapOne<GetCitiesIdsByGovernorateIDQuery>());
                if (!citiesIds.Data.IsNullOrEmpty())
                {
                    foreach (var citiesId in citiesIds.Data)
                    {
                         await _mediator.Send(new DeactiveCityCommand(citiesId));
                    }
                }
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }
            var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

            return await Task.FromResult(result);
        }
    }

}
