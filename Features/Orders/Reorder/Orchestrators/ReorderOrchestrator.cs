using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Catrs.AddCart.Orchestrators;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
namespace KOG.ECommerce.Features.Orders.Reorder.Orchestrators
{
    public record ReorderOrchestrator(string OrderId) :IRequestBase<bool>;
    public class ReorderOrchestratorsHandler : RequestHandlerBase<Order, ReorderOrchestrator, bool>
    {
        public ReorderOrchestratorsHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public  async override Task<RequestResult<bool>> Handle(ReorderOrchestrator request, CancellationToken cancellationToken)
        {
            var orders = await _mediator.Send(new GetOrderByIDQuery(request.OrderId));
            var cartProductsResult = await _mediator.Send(request.MapOne<GetAllOrderItemByIDQuery>());
            if (cartProductsResult.Data == null || !cartProductsResult.Data.Any())
            {
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            }

            foreach (var item in cartProductsResult.Data)
            {
                var addResult = await _mediator.Send(new AddProductToCartOrchestrator(item.ProductId, item.Quantity));
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
