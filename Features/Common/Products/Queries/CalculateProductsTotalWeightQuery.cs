using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CalculateProductsTotalLiterQuery(List<ProductIDandQuantityDTO> Products) : IRequestBase<double>;
    public class CalculateProductsTotalLiterQueryHandler : RequestHandlerBase<Product, CalculateProductsTotalLiterQuery, double>
    {
        public CalculateProductsTotalLiterQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<double>> Handle(CalculateProductsTotalLiterQuery request, CancellationToken cancellationToken)
        {
            if (request.Products == null || !request.Products.Any())
            {
                return RequestResult<double>.Failure(ErrorCode.ProductNotFound);
            }

            var productIds = request.Products.Select(p => p.ProductId).ToList();

            var products = _repository.Get(p => productIds.Contains(p.ID) && p.IsActivePoint)
                                      .Select(p => new { p.ID, p.Liter })
                                      .ToList();

            if (!products.Any())
            {
                return RequestResult<double>.Failure(ErrorCode.ProductNotFound);
            }

            double totalLiter = request.Products
                .Join(products,
                      requestItem => requestItem.ProductId,
                      dbProduct => dbProduct.ID,
                      (requestItem, dbProduct) => dbProduct.Liter * requestItem.Quantity)
                .Sum();

            return RequestResult<double>.Success(totalLiter);
        }
    }
}
