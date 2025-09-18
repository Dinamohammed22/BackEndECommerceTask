using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.Common.WishLists.AddWishListByClientId.Commands
{
    public record AddWishListByClientIdCommand(string ClientId) : IRequestBase<bool>;
    public class AddWishListByClientIdCommandHandler : RequestHandlerBase<Wishlist, AddWishListByClientIdCommand, bool>
    {
        public AddWishListByClientIdCommandHandler(RequestHandlerBaseParameters<Wishlist> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddWishListByClientIdCommand request, CancellationToken cancellationToken)
        {
            bool existWishlist = _repository.Any(w => w.ID == request.ClientId);
            if (!existWishlist)
            {
                Wishlist wishlist = new Wishlist { ID = request.ClientId };
                _repository.Add(wishlist);
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Success(false);
        }
    }
}
