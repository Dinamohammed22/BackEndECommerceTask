using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Companies.ActiveCompany.Commands;
using KOG.ECommerce.Features.Products.ActivateProducts.Commands;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.ActiveCompany.Orchestrators
{
    public record ActiveCompanyOrchestrator(string ID) : IRequestBase<bool>;
    public class ActiveCompanyOrchestratorHandler : RequestHandlerBase<Company, ActiveCompanyOrchestrator, bool>
    {
        public ActiveCompanyOrchestratorHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveCompanyOrchestrator request, CancellationToken cancellationToken)
        {
            var CheckProducts = await _mediator.Send(new CheckIfCompanyHasProductsQuery(request.ID));
            if (CheckProducts.Data )
            {
                var ProductIds = await _mediator.Send(new GetProductIdsByCompanyIdQuery(request.ID));


                foreach (var item in ProductIds.Data)
                {
                    await _mediator.Send(new ActivateProductsCommand(item.ID));
                }
            }
            var activeCompany = await _mediator.Send(request.MapOne<ActiveCompanyCommand>());
            if (!activeCompany.IsSuccess)
            {
                return RequestResult<bool>.Failure(activeCompany.ErrorCode);
            }
            var userResult = await _mediator.Send(new ActivateUserCommand(request.ID));

            if (!userResult.IsSuccess)
            {
                return RequestResult<bool>.Failure(userResult.ErrorCode);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
