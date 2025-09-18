using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CalculateProductsTotalPointsQuery(List<ProductIDandQuantityDTO> Products) : IRequestBase<int>;
    public class CalculateProductsTotalPointsQueryHandler : RequestHandlerBase<Product, CalculateProductsTotalPointsQuery, int>
    {
        public CalculateProductsTotalPointsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<int>> Handle(CalculateProductsTotalPointsQuery request, CancellationToken cancellationToken)
        {
            if (request.Products == null || !request.Products.Any())
            {
                return RequestResult<int>.Failure(ErrorCode.ProductNotFound);
            }

            var productIds = request.Products.Select(p => p.ProductId).ToList();

            var products = _repository.Get(p => productIds.Contains(p.ID) && p.IsActivePoint)
                                      .Select(p => new { p.ID, p.NumberOfPoints })
                                      .ToList();

            if (!products.Any())
            {
                return RequestResult<int>.Failure(ErrorCode.ProductNotFound);
            }

            int totalPoints = request.Products
                .Join(products,
                      requestItem => requestItem.ProductId,
                      dbProduct => dbProduct.ID,
                      (requestItem, dbProduct) => dbProduct.NumberOfPoints * requestItem.Quantity)
                .Sum();

            return RequestResult<int>.Success(totalPoints);
        }
    }
}