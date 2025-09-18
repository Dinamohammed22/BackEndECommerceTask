using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.WishlistProducts.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.WishlistProducts.Queries
{
    public record GetAllProductFromWishlistQuery() : IRequestBase<IEnumerable<GetAllProductFromWishlistDTO>>;

    public class GetAllProductFromWishlistQueryHandler : RequestHandlerBase<WishlistProduct, GetAllProductFromWishlistQuery, IEnumerable<GetAllProductFromWishlistDTO>>
    {
        public GetAllProductFromWishlistQueryHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetAllProductFromWishlistDTO>>> Handle(GetAllProductFromWishlistQuery request, CancellationToken cancellationToken)
        {
            var WishlistProducts = _repository.Get(c => c.WishlistId == _userState.UserID)
                                               .Include(wp => wp.Product).ThenInclude(c=>c.Company) // Include the related Product entity
                                               .ToList();

            //if (WishlistProducts == null || !WishlistProducts.Any())
            //{
            //    return RequestResult<IEnumerable<GetAllProductFromWishlistDTO>>.Failure(ECommerce.Common.Enums.ErrorCode.NotFound);
            //}

            var productViewDtos = new List<GetAllProductFromWishlistDTO>();

            foreach (var wishlistProduct in WishlistProducts)
            {
                // Fetch the media for the related Product
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(wishlistProduct.ProductId, SourceType.Product));

                // Manually map the properties of WishlistProduct and Product to the DTO
                var productWithMedia = new GetAllProductFromWishlistDTO(
                    wishlistProduct.ProductId,   // Assuming you want the Product ID
                    wishlistProduct.Product.Name, // Map Product.Name to DTO Name
                    wishlistProduct.Product.Price, 
                    mediaResult.IsSuccess ? mediaResult.Data : string.Empty,
                    wishlistProduct.Product.MaximumQuantity,
                    wishlistProduct.Product.MinimumQuantity,
                    wishlistProduct.Product.Company.Name,
                    wishlistProduct.Product.CompanyId
                );

                productViewDtos.Add(productWithMedia);
            }

            return RequestResult<IEnumerable<GetAllProductFromWishlistDTO>>.Success(productViewDtos);
        }
    }
}
