using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.WishLists.AddWishListByClientId.Commands;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.DeactivateWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Clients.BulkDeactivateClients.Commands
{
    public record BulkDeactivateClientsCommand(List<string> Ids) : IRequestBase<bool>;
    public class BulkDeactivateClientsCommandHandler : RequestHandlerBase<Client, BulkDeactivateClientsCommand, bool>
    {
        public BulkDeactivateClientsCommandHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateClientsCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var exsitWishList = await _mediator.Send(new AddWishListByClientIdCommand(id));
                var wishlist = await _mediator.Send(new DeactivateWishlistCommand(id));
                if (!wishlist.IsSuccess)
                {
                    return RequestResult<bool>.Failure(wishlist.ErrorCode);
                }
                var client = await _mediator.Send(new DeactivateUserCommand(id));
                if (!client.IsSuccess)
                {
                    return RequestResult<bool>.Failure(client.ErrorCode);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
