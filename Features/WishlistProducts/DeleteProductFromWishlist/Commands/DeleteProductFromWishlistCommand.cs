using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.WishlistProducts.DeleteProductFromWishlist.Commands
{
    public record DeleteProductFromWishlistCommand(string productId) : IRequestBase<bool>;
    public class DeleteProductFromWishlistCommandHandler : RequestHandlerBase<WishlistProduct, DeleteProductFromWishlistCommand, bool>
    {
        public DeleteProductFromWishlistCommandHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteProductFromWishlistCommand request, CancellationToken cancellationToken)
        {
            // Check if the product exists
            var checkProductResult = await _mediator.Send(new CheckProductById(request.productId));

            if (checkProductResult.Data.IsNullOrEmpty())
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            }

            var wishlistProduct = await _repository
                .Get(wp =>wp.ProductId == request.productId && wp.WishlistId == _userState.UserID)
                .FirstOrDefaultAsync();

            if (wishlistProduct == null)
                return RequestResult<bool>.Failure(ErrorCode.ProductWishlistNotFound);

            _repository.Delete(wishlistProduct);
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);

            return await Task.FromResult(result);

        }
    }


}
