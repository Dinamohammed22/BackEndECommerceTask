using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.CartProducts.RemoveProductFromCart;
using KOG.ECommerce.Features.Carts.DeleteCart.Commands;
using KOG.ECommerce.Features.Clients.DeleteClient.Commands;
using KOG.ECommerce.Features.Common.Users.DeleteUser.Commands;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Features.WishlistProducts.RemoveAllProductsFromWishlist;
using KOG.ECommerce.Features.Wishlists.DeleteWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Clients.DeleteClient.Orchestrator
{
    public record DeleteClientOrchestrator(string? ID) : IRequestBase<bool>;
    public class DleteClientOrchestratorHandler : RequestHandlerBase<Client, DeleteClientOrchestrator, bool>
    {
        public DleteClientOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(DeleteClientOrchestrator request, CancellationToken cancellationToken)
        {
            var ID = request.ID is null ? _userState.UserID : request.ID;

            var Client = await  _repository.Get(c => c.ID == request.ID).Include(c=> c.Cart).Include(c=>c.WishList).FirstOrDefaultAsync();
            if (Client.Cart != null)
            {
                await _mediator.Send(new RemoveAllProductsFromCartCommand(request.ID));
               await _mediator.Send(new DeleteCartCommand(request.ID));
            }

            if (Client.WishList != null)
            {
                await _mediator.Send(new RemoveAllProductsFromWishlistCommand(request.ID));
                await _mediator.Send(new DeleteWishlistCommand(request.ID));
            }

            var client = await _mediator.Send(new DeactivateUserCommand(ID));
            if (!client.IsSuccess)
            {
                return RequestResult<bool>.Failure(client.ErrorCode);
            }

            await _mediator.Send(new DeleteClientCommand(request.ID));  
            await _mediator.Send(new DeleteUserCommand(request.ID));
            return RequestResult<bool>.Success(true);
        }
    }
}
