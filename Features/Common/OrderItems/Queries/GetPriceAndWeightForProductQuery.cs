using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Models.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record GetPriceAndWeightForProductQuery(string productId):IRequestBase<GetPriceAndWeightForProductDTO>;
    public class GetPriceAndWeightForProductQueryHandler : RequestHandlerBase<OrderItem, GetPriceAndWeightForProductQuery, GetPriceAndWeightForProductDTO>
    {
        public GetPriceAndWeightForProductQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetPriceAndWeightForProductDTO>> Handle(GetPriceAndWeightForProductQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.Get(oi => oi.ProductId == request.productId).ToListAsync();
            var resultDto = new GetPriceAndWeightForProductDTO
            {
                TotalPrice = items.Sum(oi => oi.Price),
                TotalLiter = items.Sum(oi => oi.Liter)
            };
            return RequestResult<GetPriceAndWeightForProductDTO>.Success(resultDto);
        }
    }
}
