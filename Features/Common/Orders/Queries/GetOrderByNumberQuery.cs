using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrderByNumberQuery(string OrderNumber):IRequestBase<GetOrderByNumberDTO>;
    public class GetOrderByNumberQueryHandler : RequestHandlerBase<Order, GetOrderByNumberQuery, GetOrderByNumberDTO>
    {
        public GetOrderByNumberQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderByNumberDTO>> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(o => o.OrderNumber == request.OrderNumber)
                .Include(o => o.Client) // Include Client
                .Include(o => o.OrderItems) // Include OrderItems
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return RequestResult<GetOrderByNumberDTO>.Failure(ErrorCode.OrderNotFound);
            }

            var orderItemsResult = await _mediator.Send(new GetAllOrderItemsWithImagesQuery(order.ID), cancellationToken);
            if (!orderItemsResult.IsSuccess)
            {
                return RequestResult<GetOrderByNumberDTO>.Failure(orderItemsResult.ErrorCode);
            }

            //calculate total Liter
            double TotalLiter = 0;

            foreach (var product in orderItemsResult.Data)
            {
                TotalLiter += product.ItemLiter * product.Quantity;
            }

            var orderDTO = new GetOrderByNumberDTO(
                OrderNumber: order.OrderNumber,
                TotalPrice: order.TotalPrice,
                TotalNetPrice: order.TotalNetPrice,
                Status: order.Status,
                CreatedDate: order.CreatedDate,
                OrderId: order.ID,
                CustomerName: order.Client?.Name, 
                Mobile: order.Client?.Mobile,
                Email: order.Client?.Email,
                OrderItems: orderItemsResult.Data.ToList(),
                TotalLiter: TotalLiter
            );

            return RequestResult<GetOrderByNumberDTO>.Success(orderDTO);
        }

    }
}
