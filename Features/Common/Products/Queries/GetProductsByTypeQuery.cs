using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using System.Collections.Generic;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductsByTypeQuery(GetProductType GetProductType, int NumberOfProducts = 3) : IRequestBase<IEnumerable<ProductViewDTO>>;

    public class GetProductsByTypeQueryHandler : RequestHandlerBase<Product, GetProductsByTypeQuery, IEnumerable<ProductViewDTO>>
    {
        public GetProductsByTypeQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            var result = request.GetProductType switch
            {
                GetProductType.FavoriteProducts => await _mediator.Send(request.MapOne<GetFavoriteProductsQuery>()),
                GetProductType.NewProducts => await _mediator.Send(request.MapOne<GetNewProductsQuery>()),
                GetProductType.BestSellerProducts => await _mediator.Send(request.MapOne<GetBestSellerProductsQuery>()),
            };

            if (result.IsSuccess)
            {
                return RequestResult<IEnumerable<ProductViewDTO>>.Success(result.Data);
            }
            else
            {
                return RequestResult<IEnumerable<ProductViewDTO>>.Failure(result.ErrorCode);
            }
        }
    }
}
