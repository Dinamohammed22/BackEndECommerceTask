using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Products.RestockProduct.commands;
using KOG.ECommerce.Features.WishlistProducts.NotifyClientsOfRestockedWishlistProducts.Orchestrator;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.RestockProduct.orchestrator
{
    public record RestockProductOrchestrator(string ProductID, int Quantity = 1) : IRequestBase<bool>;
    public class RestockProductOrchestratorHandler : RequestHandlerBase<Product, RestockProductOrchestrator, bool>
    {
        public RestockProductOrchestratorHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(RestockProductOrchestrator request, CancellationToken cancellationToken)
        {
            var checkRestock = await _mediator.Send(new RestockProductCommand(request.ProductID,request.Quantity));
            if (!checkRestock.IsSuccess)
            {
                return RequestResult<bool>.Failure(checkRestock.ErrorCode);
            }

            var checkNotify = await _mediator.Send(new NotifyClientsOfRestockedWishlistProductsOrchestrator(request.ProductID));
            if (!checkNotify.IsSuccess)
            {
                return RequestResult<bool>.Failure(checkNotify.ErrorCode);
            }

            return RequestResult<bool>.Success();
        }
    }

}
