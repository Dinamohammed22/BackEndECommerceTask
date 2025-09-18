using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetAllOrderItemByIDQuery(string OrderId):IRequestBase<IEnumerable<OrderItemDTO>>;
    public class GetAllOrderItemByIDQueryHandler : RequestHandlerBase<OrderItem, GetAllOrderItemByIDQuery, IEnumerable<OrderItemDTO>>
    {
        public GetAllOrderItemByIDQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<OrderItemDTO>>> Handle(GetAllOrderItemByIDQuery request, CancellationToken cancellationToken)
        {
            var orderItems=  _repository.GetWithDeleted()
                .Where(o => o.OrderId == request.OrderId)
                .Include(oi => oi.Product)
                .MapList<OrderItemDTO>();
            if (!orderItems.Any()) {
                return RequestResult<IEnumerable<OrderItemDTO>>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<IEnumerable<OrderItemDTO>>.Success(orderItems);
        }
    }
}
