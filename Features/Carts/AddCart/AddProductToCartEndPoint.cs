using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Features.Cities.CreateCity;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Catrs.AddCart.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Catrs.AddProductToCart
{
    public class AddProductToCartEndPoint : EndpointBase<AddProductToCartRequestViewModel, AddProductToCartResponseViewModel>
    {
        public AddProductToCartEndPoint(EndpointBaseParameters<AddProductToCartRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddProductToCart })]
        public async Task<EndPointResponse<AddProductToCartResponseViewModel>> Post(AddProductToCartRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddProductToCartOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<AddProductToCartResponseViewModel>.Success(new AddProductToCartResponseViewModel(), "Product Added To Cart successfully.");
            }
            return EndPointResponse<AddProductToCartResponseViewModel>.Failure(result.ErrorCode);

        }

    }
}
