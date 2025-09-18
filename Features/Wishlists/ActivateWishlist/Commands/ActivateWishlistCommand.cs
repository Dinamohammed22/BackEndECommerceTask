using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.Wishlists.ActivateWishlist.Commands
{
    public record ActivateWishlistCommand(string ID):IRequestBase<bool>;
    public class ActivateWishlistCommandHandler : RequestHandlerBase<Wishlist, ActivateWishlistCommand, bool>
    {
        public ActivateWishlistCommandHandler(RequestHandlerBaseParameters<Wishlist> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateWishlistCommand request, CancellationToken cancellationToken)
        {
            var check = _repository.Any(c => c.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Wishlist wishlist = new Wishlist { ID = request.ID };
            wishlist.IsActive = true;
            _repository.SaveIncluded(wishlist, nameof(wishlist.IsActive));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
