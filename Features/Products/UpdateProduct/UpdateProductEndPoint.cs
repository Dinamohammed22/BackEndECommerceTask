using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.UpdateProduct.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.UpdateProduct
{
    public class UpdateProductEndPoint : EndpointBase<UpdateProductRequestViewModel, UpdateProductResponseViewModel>
    {
        public UpdateProductEndPoint(EndpointBaseParameters<UpdateProductRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditProduct })]
        public async Task<EndPointResponse<UpdateProductResponseViewModel>> UpdateProduct(UpdateProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<UpdateProductOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<UpdateProductResponseViewModel>.Success(new UpdateProductResponseViewModel(), "Product Updated successfully");
            else
                return EndPointResponse<UpdateProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
