using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.Wishlists.AddWishlist.Commands
{
    public record AddWishlistCommand() : IRequestBase<bool>;

    public class AddWishlistCommandHandler : RequestHandlerBase<Wishlist, AddWishlistCommand, bool>
    {
        public AddWishlistCommandHandler(RequestHandlerBaseParameters<Wishlist> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddWishlistCommand request, CancellationToken cancellationToken)
        {
            bool existWishlist = _repository.Any(w => w.ID == _userState.UserID);
            if (!existWishlist)
            {
                Wishlist wishlist = new Wishlist { ID = _userState.UserID };
                _repository.Add(wishlist);
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Success(false);
        }
    }
}
