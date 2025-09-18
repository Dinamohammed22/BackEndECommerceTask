using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Commands
{
    public record AddProductToWishlistCommand(string ProductId) : IRequestBase<bool>;

    public class AddProductToWishlistCommandHandler : RequestHandlerBase<WishlistProduct, AddProductToWishlistCommand, bool>
    {
        public AddProductToWishlistCommandHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddProductToWishlistCommand request, CancellationToken cancellationToken)
        {           
            // Check if the product exists
            var checkProductResult = await _mediator.Send(new CheckProductById(request.ProductId));

            if (checkProductResult.Data.IsNullOrEmpty())
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            }

            // Check if the product is already in the wishlist
            var existingProduct = await _repository.FirstOrDefaultAsync(p => p.ProductId == request.ProductId && p.WishlistId == _userState.UserID);
            if (existingProduct == null)
            {
                // Add the product to the wishlist if it does not exist
                var newProduct = new WishlistProduct
                {
                    ProductId = request.ProductId,
                    WishlistId = _userState.UserID
                };

                await _repository.AddAsync(newProduct);
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true, "Product Added To Wishlist successfully");

            }
            else
            {
                // Save the existing product to ensure it stays up to date
                _repository.SaveIncluded(existingProduct, nameof(existingProduct.UpdatedBy), nameof(existingProduct.UpdatedDate));
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true,"product already exist in the wishlist");
            }

        }
    }

}
