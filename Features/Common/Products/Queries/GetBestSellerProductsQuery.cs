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
    public record GetBestSellerProductsQuery(int NumberOfProducts = 3):IRequestBase<IEnumerable<ProductViewDTO>>;
    public class GetBestSellerProductsQueryHandler : RequestHandlerBase<Product, GetBestSellerProductsQuery, IEnumerable<ProductViewDTO>>
    {
        public GetBestSellerProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetBestSellerProductsQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<IEnumerable<ProductViewDTO>>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;


            var bestSellers = await _repository.Get(p=> p.IsActive &&
                     p.Company.IsActive &&
                     p.Company.CompanyGovernorates
                          .Any(cg => cg.GovernorateId == governorateId))
                .Include(p => p.OrderItems).Include(p=>p.Company)
                .ThenInclude(c => c.CompanyGovernorates)
                .Where(p => p.OrderItems.Any())
                .Select(p => new
                {
                    Product = p,
                    TotalQuantity = p.OrderItems.Sum(o => o.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(request.NumberOfProducts)
                .Select(x => new ProductViewDTO
                {
                    ID = x.Product.ID,
                    ProductName = x.Product.Name,
                    Price = x.Product.Price,
                    MaximumQuantity = x.Product.MaximumQuantity,
                    MinimumQuantity = x.Product.MinimumQuantity,
                    ProductQuantity=x.Product.Quantity,
                    CompanyId = x.Product.CompanyId,
                    CompanyName = x.Product.Company.Name,
                })
                .ToListAsync(cancellationToken);

            //add media to the best seller list

            var productViewDtos=new List<ProductViewDTO>();
            foreach (var product in bestSellers)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(product.ID, SourceType.Product));
                var productWithMedia = product.MapOne<ProductViewDTO>() with
                {
                    Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
                };
                productViewDtos.Add(productWithMedia);
            }

            return RequestResult<IEnumerable<ProductViewDTO>>.Success(productViewDtos);

        }
    }
}
