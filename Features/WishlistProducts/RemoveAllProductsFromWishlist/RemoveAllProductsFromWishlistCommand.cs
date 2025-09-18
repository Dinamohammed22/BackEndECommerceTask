using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.WishlistProducts.RemoveAllProductsFromWishlist
{
    public record RemoveAllProductsFromWishlistCommand(string? WishListId) : IRequestBase<bool>;

    public class RemoveAllProductsFromWishlistCommandHandler : RequestHandlerBase<WishlistProduct, RemoveAllProductsFromWishlistCommand, bool>
    {
        public RemoveAllProductsFromWishlistCommandHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(RemoveAllProductsFromWishlistCommand request, CancellationToken cancellationToken)
        {
            var ID = request.WishListId is null ? _userState.UserID : request.WishListId;

            var WishlistProducts =  _repository.Get(cp => cp.WishlistId == ID ).ToList();

            if (WishlistProducts == null || !WishlistProducts.Any())
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            foreach (var WishlistProduct in WishlistProducts)
            {
                _repository.Delete(WishlistProduct);
            }

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
