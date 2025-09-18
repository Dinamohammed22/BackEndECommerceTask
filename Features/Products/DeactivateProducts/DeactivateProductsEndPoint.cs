using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Products.DeactivateProducts.Commands;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.DeactivateProducts
{
    public class DeactivateProductsEndPoint : EndpointBase<DeactivateProductsRequestViewModel, DeactivateProductsResponseViewModel>
    {
        public DeactivateProductsEndPoint(EndpointBaseParameters<DeactivateProductsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateProduct })]
        public async Task<EndPointResponse<DeactivateProductsResponseViewModel>> DeactiveProduct(DeactivateProductsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivateProductsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivateProductsResponseViewModel>.Success(new DeactivateProductsResponseViewModel(), "Product Deactivated successfully");
            else
                return EndPointResponse<DeactivateProductsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
