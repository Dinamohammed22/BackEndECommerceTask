using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.CartProducts.GetAllProductAtCart;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Features.Common.WishlistProducts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.WishlistProducts.GetAllProductFromWishlist
{
    public class GetAllProductFromWishlistEndPoint : EndpointBase<GetAllProductFromWishlistRequestViewModel, GetAllProductFromWishlistResponseViewModel>
    {
        public GetAllProductFromWishlistEndPoint(EndpointBaseParameters<GetAllProductFromWishlistRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllProductFromWishlist })]
        public async Task<EndPointResponse<IEnumerable<GetAllProductFromWishlistResponseViewModel>>> GetAll()
        {

            var result = await _mediator.Send(new GetAllProductFromWishlistQuery());

            IEnumerable<GetAllProductFromWishlistResponseViewModel> response = result.Data.MapList<GetAllProductFromWishlistResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<IEnumerable<GetAllProductFromWishlistResponseViewModel>>.Success(response, "Products in the Wishlist returned successfully");
            else
                return EndPointResponse<IEnumerable<GetAllProductFromWishlistResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
