using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrderDetailsQuery(string OrderID):IRequestBase<GetOrderDetailsDTO>;
    public class GetOrderDetailsQueryHandler : RequestHandlerBase<Order, GetOrderDetailsQuery, GetOrderDetailsDTO>
    {
        public GetOrderDetailsQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderDetailsDTO>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(o => o.ID == request.OrderID).FirstOrDefaultAsync();

            if (order == null)
            {
                return RequestResult<GetOrderDetailsDTO>.Failure(ErrorCode.OrderNotFound);
            }
            var orderItemsResult = await _mediator.Send(new GetAllOrderItemsWithImagesQuery(order.ID));
            if (!orderItemsResult.IsSuccess)
            {
                return RequestResult<GetOrderDetailsDTO>.Failure(orderItemsResult.ErrorCode);
            }
            var orderDTO = new GetOrderDetailsDTO(
               OrderNumber: order.OrderNumber,
               OrderID: order.ID,
               Status: order.Status,
               TotalPrice:order.TotalPrice,
               TotalNetPrice:order.TotalNetPrice,
               TotalQuantity:order.TotalQuantity,
               Items: orderItemsResult.Data.ToList()
           );

            return RequestResult<GetOrderDetailsDTO>.Success(orderDTO);
        }
    }

}
