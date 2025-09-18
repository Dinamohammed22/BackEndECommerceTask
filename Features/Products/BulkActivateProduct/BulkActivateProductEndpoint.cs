using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Products.BulkActivateProduct.Orchistrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Products.BulkActivateProduct
{
    public class BulkActivateProductEndpoint : EndpointBase<BulkActivateProductRequestViewModel, BulkActivateProductResponseViewModel>
    {
        public BulkActivateProductEndpoint(EndpointBaseParameters<BulkActivateProductRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateProduct })]
        public async Task<EndPointResponse<BulkActivateProductResponseViewModel>> BulkActivateProduct(BulkActivateProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateProductOrchistrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateProductResponseViewModel>.Success(new BulkActivateProductResponseViewModel(), "All Products Activated successfully");
            else
                return EndPointResponse<BulkActivateProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
