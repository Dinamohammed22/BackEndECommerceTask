using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.Wishlists.DeleteWishlist.Commands
{
    public record DeleteWishlistCommand(string ID) : IRequestBase<bool>;
    public class DeleteWishlistCommandsHandler : RequestHandlerBase<Wishlist, DeleteWishlistCommand, bool>
    {
        public DeleteWishlistCommandsHandler(RequestHandlerBaseParameters<Wishlist> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Wishlist Wishlist = new Wishlist();
            Wishlist.ID = request.ID;
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
