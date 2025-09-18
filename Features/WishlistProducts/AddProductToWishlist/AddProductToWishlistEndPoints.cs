using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.WishlistProducts.AddProductToWishlist
{
    public class AddProductToWishlistEndPoints : EndpointBase<AddProductToWishlistRequestViewModel, AddProductToWishlistResponseViewModel>
    {
        public AddProductToWishlistEndPoints(EndpointBaseParameters<AddProductToWishlistRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddProductToWishlist })]
        public async Task<EndPointResponse<AddProductToWishlistResponseViewModel>> AddProductToWishlist(AddProductToWishlistRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddProductToWishlistOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<AddProductToWishlistResponseViewModel>.Success(new AddProductToWishlistResponseViewModel() , result.Message);
            else
                return EndPointResponse<AddProductToWishlistResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
