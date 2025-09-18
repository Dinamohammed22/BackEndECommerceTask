using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetOrderIdsForProductsQuery(IEnumerable<GetProductIdsByCompanyIdDTO> IDs) : IRequestBase<IEnumerable<string>>;
    public class GetOrderIdsForProductsQueryHandler : RequestHandlerBase<OrderItem, GetOrderIdsForProductsQuery, IEnumerable<string>>
    {
        public GetOrderIdsForProductsQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<string>>> Handle(GetOrderIdsForProductsQuery request, CancellationToken cancellationToken)
        {
            var OrderIds= new List<string>();
            foreach (var item in request.IDs)
            {
                var orderId = await _repository.Get(oi => oi.ProductId == item.ID).Select(oi=>oi.OrderId).FirstOrDefaultAsync();
                OrderIds.Add(orderId);
            }
            return RequestResult<IEnumerable<string>>.Success(OrderIds);

        }
    }
}
