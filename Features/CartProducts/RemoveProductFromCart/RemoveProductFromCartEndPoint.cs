using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.CartProducts.RemoveProductFromCart.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.CartProducts.RemoveProductFromCart
{
    public class RemoveProductFromCartEndPoint : EndpointBase<RemoveProductFromCartRequestViewModel, RemoveProductFromCartResponseViewModel>
    {
        public RemoveProductFromCartEndPoint(EndpointBaseParameters<RemoveProductFromCartRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.RemoveProductFromCart })]
        public async Task<EndPointResponse<RemoveProductFromCartResponseViewModel>> RemoveProductFromCart(RemoveProductFromCartRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<RemoveProductFromCartCommand>());
            if (result.IsSuccess)
                return EndPointResponse<RemoveProductFromCartResponseViewModel>.Success(new RemoveProductFromCartResponseViewModel(), "Product Deleted From Cart Successfully");
            else
                return EndPointResponse<RemoveProductFromCartResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
