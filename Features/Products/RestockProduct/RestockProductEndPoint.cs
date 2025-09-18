using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.RestockProduct.orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.RestockProduct
{
    public class RestockProductEndPoint : EndpointBase<RestockProductRequestViewModel, RestockProductResponseViewModel>
    {
        public RestockProductEndPoint(EndpointBaseParameters<RestockProductRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.RestockProduct })]
        public async Task<EndPointResponse<RestockProductResponseViewModel>> RestockProduct(RestockProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<RestockProductOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<RestockProductResponseViewModel>.Success(new RestockProductResponseViewModel(), "Product Restocked successfully");
            else
                return EndPointResponse<RestockProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
