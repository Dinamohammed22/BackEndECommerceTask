using System.Linq;
using AutoMapper;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetPriceOfProductsQuery(List<GetAllProductAtCartDTO> Products) : IRequestBase<IEnumerable<GetPriceOfProductsDTO>>;

    public class GetPriceOfProductsQueryHandler : RequestHandlerBase<Product, GetPriceOfProductsQuery, IEnumerable<GetPriceOfProductsDTO>>
    {
        public GetPriceOfProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<GetPriceOfProductsDTO>>> Handle(GetPriceOfProductsQuery request, CancellationToken cancellationToken)
        {
            var productIds = request.Products.Select(p => p.ProductId).ToList();
            var products =  _repository
                .Get(p => productIds.Contains(p.ID)).ToList();

            if (!products.Any())
            {
                return RequestResult<IEnumerable<GetPriceOfProductsDTO>>.Failure(ErrorCode.NotFound);
            }


            var productsDto = products.Select(product =>
            {
                var quantity = request.Products.First(p => p.ProductId == product.ID).Quantity;
                return new GetPriceOfProductsDTO(product.ID, quantity, product.Price,product.Liter,product.NumberOfPoints);
            });

            return RequestResult<IEnumerable<GetPriceOfProductsDTO>>.Success(productsDto);
        }
    }
}
