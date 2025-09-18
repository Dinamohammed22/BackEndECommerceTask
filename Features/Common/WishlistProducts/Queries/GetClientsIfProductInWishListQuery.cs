using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.WishlistProducts.Queries
{
    public record GetClientsIfProductInWishListQuery(string ProductID) : IRequestBase<List<string>>;

    public class GetClientsIfProductInWishListQueryHandler : RequestHandlerBase<WishlistProduct, GetClientsIfProductInWishListQuery, List<string>>
    {
        public GetClientsIfProductInWishListQueryHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters)
            : base(requestParameters)
        {
        }

        public override async Task<RequestResult<List<string>>> Handle(GetClientsIfProductInWishListQuery request, CancellationToken cancellationToken)
        {
            var wishListIDs = await _repository
                .Get(wp => wp.ProductId == request.ProductID)
                .Select(wp => wp.WishlistId)
                .ToListAsync();
                 
            var activeWishListIDs = new List<string>();

            foreach (var id in wishListIDs)
            {
                var check = await _mediator.Send(new CheckUserActivationQuery(id), cancellationToken);
                if (check.Data)
                {
                    activeWishListIDs.Add(id);
                }
            }

            return RequestResult<List<string>>.Success(activeWishListIDs);
        }
    }
}
