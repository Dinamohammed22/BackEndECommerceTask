using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.Wishlists.DeactivateWishlist.Commands
{
    public record DeactivateWishlistCommand(string ID) : IRequestBase<bool>;
    public class DeactivateWishlistCommandHandler : RequestHandlerBase<Wishlist, DeactivateWishlistCommand, bool>
    {
        public DeactivateWishlistCommandHandler(RequestHandlerBaseParameters<Wishlist> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactivateWishlistCommand request, CancellationToken cancellationToken)
        {
            var check = _repository.Any(c => c.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Wishlist wishlist = new Wishlist { ID = request.ID };
            wishlist.IsActive = false;
            _repository.SaveIncluded(wishlist, nameof(wishlist.IsActive));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
