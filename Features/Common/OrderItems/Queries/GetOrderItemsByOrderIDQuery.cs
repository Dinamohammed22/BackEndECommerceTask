using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetOrderItemsByOrderIDQuery(string OrderId) : IRequestBase<IEnumerable<OrderItemWithItemNameDTO>>;
    public class GetOrderItemsByOrderIDQueryHandler : RequestHandlerBase<OrderItem, GetOrderItemsByOrderIDQuery, IEnumerable<OrderItemWithItemNameDTO>>
    {
        public GetOrderItemsByOrderIDQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<OrderItemWithItemNameDTO>>> Handle(GetOrderItemsByOrderIDQuery request, CancellationToken cancellationToken)
        {
            var orderItemEntities = _repository.Get(o => o.OrderId == request.OrderId)
                .Include(o => o.Product)
                .ThenInclude(p => p.Company)
                .ToList();

            var orderItems = orderItemEntities.MapList<OrderItemWithItemNameDTO>();
            if (!orderItems.Any())
            {
                return RequestResult<IEnumerable<OrderItemWithItemNameDTO>>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<IEnumerable<OrderItemWithItemNameDTO>>.Success(orderItems);
        }
    }
}
