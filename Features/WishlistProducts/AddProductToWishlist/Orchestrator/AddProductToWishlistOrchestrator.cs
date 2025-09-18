using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Commands;
using KOG.ECommerce.Features.Wishlists.AddWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Wishlists;

namespace KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Orchestrator
{
    public record AddProductToWishlistOrchestrator(string ProductId) : IRequestBase<bool>;

    public class AddProductToWishlistOrchestratorHandler : RequestHandlerBase<Wishlist, AddProductToWishlistOrchestrator, bool>
    {
        public AddProductToWishlistOrchestratorHandler(RequestHandlerBaseParameters<Wishlist> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(AddProductToWishlistOrchestrator request, CancellationToken cancellationToken)
        {
            var WishlistRequest = await _mediator.Send(new AddWishlistCommand());
            var AddProduct = await _mediator.Send(request.MapOne<AddProductToWishlistCommand>());
            if (!AddProduct.IsSuccess)
            {
                return RequestResult<bool>.Failure(ErrorCode.ProductNotFound);
            }
            return RequestResult<bool>.Success(AddProduct.Data , AddProduct.Message);
        }
    }
}
