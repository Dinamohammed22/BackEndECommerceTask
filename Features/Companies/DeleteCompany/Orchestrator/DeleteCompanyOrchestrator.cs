using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.CreateClient.Commands;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Features.Common.Users.DeleteUser.Commands;
using KOG.ECommerce.Features.Companies.DeleteCompany.Command;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.DeleteCompany.Orchestrator
{
    public record DeleteCompanyOrchestrator(string Id) : IRequestBase<bool>;
    public class DeleteCompanyOrchestratorHandler : RequestHandlerBase<Company, DeleteCompanyOrchestrator, bool>
    {
        public DeleteCompanyOrchestratorHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteCompanyOrchestrator request, CancellationToken cancellationToken)
        {
            var CheckProducts = await _mediator.Send(new CheckIfCompanyHasProductsQuery(request.Id));
            if(CheckProducts.Data)
            {
                var ProductIds = await _mediator.Send(new GetProductIdsByCompanyIdQuery(request.Id));
                //check order

            }
            var userResult = await _mediator.Send(request.MapOne< DeleteUserCommand>());

            if (!userResult.IsSuccess)
            {
                return RequestResult<bool>.Failure(userResult.ErrorCode);
            }

            var result = await _mediator.Send(request.MapOne<DeleteCompanyCommand>());


            if (!result.IsSuccess)
            {
                return RequestResult<bool>.Failure(result.ErrorCode);
            }
        
            return RequestResult<bool>.Success(true);
        }
    }

}
