using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.CartProducts.Commands;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Common.WishlistProducts.Commands;
using KOG.ECommerce.Features.Companies.DeactiveCompany.Commands;
using KOG.ECommerce.Features.Products.DeactivateProducts.Commands;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Companies.DeactiveCompany.Orchestrators
{
    public record DeactiveCompanyOrchestrator(string ID) : IRequestBase<bool>;
    public class DeactiveCompanyOrchestratorHandler : RequestHandlerBase<Company, DeactiveCompanyOrchestrator, bool>
    {
        public DeactiveCompanyOrchestratorHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveCompanyOrchestrator request, CancellationToken cancellationToken)
        {
            var CheckProducts = await _mediator.Send(new CheckIfCompanyHasProductsQuery(request.ID));
            if (CheckProducts.Data)
            {
                var ProductIds = await _mediator.Send(new GetProductIdsByCompanyIdQuery(request.ID));

                var OrderIds = await _mediator.Send(new GetOrderIdsForProductsQuery(ProductIds.Data));
                var checkOrders= await _mediator.Send(new CheckIfOrdersPendingQuery(OrderIds.Data.ToList()));
                if (checkOrders.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDeactive);
                }
                foreach (var item in ProductIds.Data)
                {
                    await _mediator.Send(new DeactivateProductsCommand(item.ID));
                }
                await _mediator.Send(new CheckWithDeleteProductsCommand(ProductIds.Data));
                await _mediator.Send(new CheckWithDeleteByProductIdsCommand(ProductIds.Data));
            }
            var userResult = await _mediator.Send(new DeactivateUserCommand(request.ID));

            if (!userResult.IsSuccess) 
            {
                return RequestResult<bool>.Failure(userResult.ErrorCode);
            }

            var result = await _mediator.Send(new DeactiveCompanyCommand(request.ID));


            if (!result.IsSuccess)
            {
                return RequestResult<bool>.Failure(result.ErrorCode);
            }

            return RequestResult<bool>.Success(true);
        }
    }
}
