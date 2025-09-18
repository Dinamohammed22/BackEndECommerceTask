using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.CartProducts.RemoveProductFromCart;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByClient.Orchistrator
{
    public record PlaceOrderByClientOrchistrator(
        string? Comment, 
        string? ShippingAddressId,
        string? GovernorateId,
        string? CityId, 
        string? Street, 
        string? Landmark,
        double? Latitude,
        double? Longitude, 
        bool? IsDefualt ,
        string? BuildingData
    ) : IRequestBase<string>;

    public class PlaceOrderByClientOrchistratorrHandler : RequestHandlerBase<Order, PlaceOrderByClientOrchistrator, string>
    {
        public PlaceOrderByClientOrchistratorrHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<string>> Handle(PlaceOrderByClientOrchistrator request, CancellationToken cancellationToken)
        {
            var cartProductsResult = await _mediator.Send(new GetAllProductAtCartQuery());
            if (cartProductsResult.Data == null || !cartProductsResult.Data.Any())
            {
                return RequestResult<string>.Failure(ErrorCode.CartIsEmpty);
            }

            //call big place order orchisterator
            var OrderNumber =  await _mediator.Send(new PlaceOrderOrchestrator(
                OrderStatus.Pending,
                _userState.UserID,
                request.Comment,
                request.ShippingAddressId,
                request.GovernorateId,
                request.CityId,
                request.Street,
                request.Landmark,
                request.Latitude,
                request.Longitude,
                request.IsDefualt,
                cartProductsResult.Data,
                request.BuildingData,
                 ShippingAddressStatus.Pending
            ));

            if (!OrderNumber.IsSuccess)
            {
                return RequestResult<string>.Failure(OrderNumber.ErrorCode, OrderNumber.Message);
            }

            // Remove all products from the cart
            await _mediator.Send(new RemoveAllProductsFromCartCommand(null));

            return RequestResult<string>.Success(OrderNumber.Data);
        }
    }
}
