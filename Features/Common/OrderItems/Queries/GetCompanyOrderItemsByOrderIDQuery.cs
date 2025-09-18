using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetCompanyOrderItemsByOrderIDQuery(string OrderId) : IRequestBase<IEnumerable<OrderItemWithItemNameDTO>>;
    public class GetCompanyOrderItemsByOrderIDQueryHandler : RequestHandlerBase<OrderItem, GetCompanyOrderItemsByOrderIDQuery, IEnumerable<OrderItemWithItemNameDTO>>
    {
        public GetCompanyOrderItemsByOrderIDQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<OrderItemWithItemNameDTO>>> Handle(GetCompanyOrderItemsByOrderIDQuery request, CancellationToken cancellationToken)
        {
            var companyId = _userState.UserID;

            var orderItemEntities = await _repository.Get(o => o.OrderId == request.OrderId && o.Product.CompanyId == companyId)
                                                     .Include(o => o.Product)
                                                     .ThenInclude(p => p.Company)
                                                     .ToListAsync(cancellationToken);
            if (!orderItemEntities.Any())
            {
                return RequestResult<IEnumerable<OrderItemWithItemNameDTO>>.Failure(ErrorCode.NotFound);
            }

            var orderItems = orderItemEntities.MapList<OrderItemWithItemNameDTO>();
            return RequestResult<IEnumerable<OrderItemWithItemNameDTO>>.Success(orderItems);
        }
    }
}
