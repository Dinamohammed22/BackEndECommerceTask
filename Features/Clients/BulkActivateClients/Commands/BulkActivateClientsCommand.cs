using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.WishLists.AddWishListByClientId.Commands;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.ActivateWishlist.Commands;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.BulkActivateClients.Commands
{
    public record BulkActivateClientsCommand(List<string> Ids):IRequestBase<bool>;
    public class BulkActivateClientsCommandHandler : RequestHandlerBase<Client, BulkActivateClientsCommand, bool>
    {
        public BulkActivateClientsCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateClientsCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var exsitWishList = await _mediator.Send(new AddWishListByClientIdCommand(id));
                var wishlist = await _mediator.Send(new ActivateWishlistCommand(id));
                if (!wishlist.IsSuccess)
                {
                    return RequestResult<bool>.Failure(wishlist.ErrorCode);
                }
                var client = await _mediator.Send(new ActivateUserCommand(id));
                if (!client.IsSuccess)
                {
                    return RequestResult<bool>.Failure(client.ErrorCode);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
