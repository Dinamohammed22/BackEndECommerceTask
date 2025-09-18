using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Products.BulkDeleteProduct.Orchistrator;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.BulkDeleteProduct
{
    public class BulkDeleteProductEndPoint : EndpointBase<BulkDeleteProductRequestViewModel, BulkDeleteProductResponseViewModel>
    {
        public BulkDeleteProductEndPoint(EndpointBaseParameters<BulkDeleteProductRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteProduct })]
        public async Task<EndPointResponse<BulkDeleteProductResponseViewModel>> BulkDeleteProduct(BulkDeleteProductRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteProductOrchistrator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteProductResponseViewModel>.Success(new BulkDeleteProductResponseViewModel(), "All Products Deleted successfully");
            else
                return EndPointResponse<BulkDeleteProductResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
