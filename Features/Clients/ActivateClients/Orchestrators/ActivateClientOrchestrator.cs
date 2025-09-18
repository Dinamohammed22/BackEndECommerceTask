using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.WishLists.AddWishListByClientId.Commands;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.ActivateWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.ActivateClients.Orchestrators
{
    public record ActivateClientOrchestrator(string ID) : IRequestBase<bool>;
    public class ActivateClientOrchestratorHandler : RequestHandlerBase<Client, ActivateClientOrchestrator, bool>
    {
        public ActivateClientOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateClientOrchestrator request, CancellationToken cancellationToken)
        {
            var exsitWishList = await _mediator.Send(new AddWishListByClientIdCommand(request.ID));
            var wishlist = await _mediator.Send(request.MapOne<ActivateWishlistCommand>());
            if (!wishlist.IsSuccess )
            {
                return RequestResult<bool>.Failure(wishlist.ErrorCode);
            }
            var client= await _mediator.Send(request.MapOne<ActivateUserCommand>());
            if (!client.IsSuccess)
            {
                return RequestResult<bool>.Failure(client.ErrorCode);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
