using KOG.ECommerce.Features.Products.BulkActivateProduct.Orchistrator;
using KOG.ECommerce.Features.Products.BulkActivateProduct;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Products.BulkDeactivateProduct.Orchistrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Products.BulkDeactivateProduct
{
    public class BulkDeactivateProductEndpoint:EndpointBase<BulkDeactivateProductRequestViewModel, BulkDeactivateProductResponseViewModel>
    {
        public BulkDeactivateProductEndpoint(EndpointBaseParameters<BulkDeactivateProductRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateProduct })]
        public async Task<EndPointResponse<BulkDeactivateProductResponseViewModel>> BulkDeactivateProduct(BulkDeactivateProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateProductOrchistrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateProductResponseViewModel>.Success(new BulkDeactivateProductResponseViewModel(), "All Products Deactivated successfully");
            else
                return EndPointResponse<BulkDeactivateProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
