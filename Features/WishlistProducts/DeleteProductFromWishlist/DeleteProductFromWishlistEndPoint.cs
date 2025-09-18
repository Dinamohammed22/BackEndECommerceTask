using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.DeleteCity;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.WishlistProducts.DeleteProductFromWishlist.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.WishlistProducts.DeleteProductFromWishlist
{
    public class DeleteProductFromWishlistEndPoint : EndpointBase<DeleteProductFromWishlistRequestViewModel, DeleteProductFromWishlistResponseViewModel>
    {
        public DeleteProductFromWishlistEndPoint(EndpointBaseParameters<DeleteProductFromWishlistRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteProductFromWishlist })]
        public async Task<EndPointResponse<DeleteProductFromWishlistResponseViewModel>> Delete(DeleteProductFromWishlistRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteProductFromWishlistCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<DeleteProductFromWishlistResponseViewModel>.Success(new DeleteProductFromWishlistResponseViewModel(), "Product Deleted From Wishlist successfully");
            }
            return EndPointResponse<DeleteProductFromWishlistResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
