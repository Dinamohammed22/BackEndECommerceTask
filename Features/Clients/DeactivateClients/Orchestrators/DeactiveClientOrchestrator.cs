using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.WishLists.AddWishListByClientId.Commands;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.DeactivateWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;

namespace KOG.ECommerce.Features.Clients.DeactivateClients.Orchestrators
{
    public record DeactiveClientOrchestrator(string ID) : IRequestBase<bool>;
    public class DeactiveClientOrchestratorHandler : RequestHandlerBase<Client, DeactiveClientOrchestrator, bool>
    {
        public DeactiveClientOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveClientOrchestrator request, CancellationToken cancellationToken)
        {
            var exsitWishList = await _mediator.Send(new AddWishListByClientIdCommand(request.ID));
            var wishlist = await _mediator.Send(request.MapOne<DeactivateWishlistCommand>());
            if (!wishlist.IsSuccess)
            {
                return RequestResult<bool>.Failure(wishlist.ErrorCode);
            }
            var client = await _mediator.Send(request.MapOne<DeactivateUserCommand>());
            if (!client.IsSuccess)
            {
                return RequestResult<bool>.Failure(client.ErrorCode);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
