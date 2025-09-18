using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Migrations;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrderStateQuery(string ID) :IRequestBase<GetOrderStateDTO>;
    public class GetOrderStateQueryHandler : RequestHandlerBase<Order, GetOrderStateQuery, GetOrderStateDTO>
    {
        public GetOrderStateQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderStateDTO>> Handle(GetOrderStateQuery request, CancellationToken cancellationToken)
        {
            var userId = _userState.UserID;

            var order = await _repository.FirstOrDefaultAsync(o => o.ID == request.ID && o.ClientId == userId);

            if (order == null)
            {
                return RequestResult<GetOrderStateDTO>.Failure();
            }

            var orderStateDto = new GetOrderStateDTO(order.Status, order.Status switch
            {
                OrderStatus.Pending => order.CreatedDate,
                OrderStatus.InProcess => order.InProcessDate,
                OrderStatus.Confirmed => order.ConfirmationDate,
                OrderStatus.Cancelled => order.CancellationDate,
                OrderStatus.Delivered => order.DeliveryDate,
                OrderStatus.Completed => order.CompletedDate,
                _ => null
            });


            return RequestResult<GetOrderStateDTO>.Success(orderStateDto);
        }
    }
}
