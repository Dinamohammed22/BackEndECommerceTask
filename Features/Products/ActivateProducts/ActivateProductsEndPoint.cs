using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.ActivateProducts.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.ActivateProducts
{
    public class ActivateProductsEndPoint : EndpointBase<ActivateProductsRequestViewModel, ActivateProductsResponseViewModel>
    {
        public ActivateProductsEndPoint(EndpointBaseParameters<ActivateProductsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActivateProduct })]
        public async Task<EndPointResponse<ActivateProductsResponseViewModel>> ActiveProduct(ActivateProductsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivateProductsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActivateProductsResponseViewModel>.Success(new ActivateProductsResponseViewModel(), "Product Activated successfully");
            else
                return EndPointResponse<ActivateProductsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
