using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetFavoriteProductsQuery(int NumberOfProducts=3):IRequestBase<IEnumerable<ProductViewDTO>>;
    public class GetFavoriteProductsQueryHandler : RequestHandlerBase<Product, GetFavoriteProductsQuery, IEnumerable<ProductViewDTO>>
    {
        public GetFavoriteProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetFavoriteProductsQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<IEnumerable<ProductViewDTO>>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var products = await _repository
                .Get(p =>
                     p.FeaturedProduct &&
                     p.IsActive &&
                     p.Company.IsActive &&
                     p.Company.CompanyGovernorates
                          .Any(cg => cg.GovernorateId == governorateId))
                .Include(p => p.Company)
                .ThenInclude(c => c.CompanyGovernorates)
                .Take(request.NumberOfProducts)
                .ToListAsync(cancellationToken);

            var dtoList = new List<ProductViewDTO>(products.Count);
            foreach (var product in products)
            {
                var media = await _mediator.Send(
                    new GetMediaForAnySourceQuery(product.ID, SourceType.Product));

                dtoList.Add(product.MapOne<ProductViewDTO>() with
                {
                    Path = media.IsSuccess ? media.Data : string.Empty
                });
            }

            return RequestResult<IEnumerable<ProductViewDTO>>.Success(dtoList);

        }
    }


}
