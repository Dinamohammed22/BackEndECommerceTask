using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Governorates.DeleteGovernorate.Orchestrators
{
    public record DeleteGovernorateOrchestrator(string ID) : IRequestBase<bool>;
    public class DeleteGovernorateOrchestratorHandler : RequestHandlerBase<Governorate, DeleteGovernorateOrchestrator, bool>
    {
        public DeleteGovernorateOrchestratorHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteGovernorateOrchestrator request, CancellationToken cancellationToken)
        {
            var IsDeleted = await _mediator.Send(request.MapOne<DeleteGovernorateCommand>());
            if (IsDeleted.IsSuccess)
            {
                var citiesIds= await _mediator.Send(request.MapOne<GetCitiesIdsByGovernorateIDQuery>());
                if(!citiesIds.Data.IsNullOrEmpty())
                {
                    foreach(var citiesId in citiesIds.Data)
                    {
                        var Deleted = await _mediator.Send(new DeleteCityCommand(citiesId));
                    }
                }
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }
            var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

            return await Task.FromResult(result);
        }
    }

}
