using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Brands.BulkDeleteBrand.Orchisterator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Brands.BulkDeleteBrand
{
    public class BulkDeleteBrandEndPoint : EndpointBase<BulkDeleteBrandRequestViewModel, BulkDeleteBrandResponseViewModel>
    {
        public BulkDeleteBrandEndPoint(EndpointBaseParameters<BulkDeleteBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteBrand })]
        public async Task<EndPointResponse<BulkDeleteBrandResponseViewModel>> BulkDeleteBrand(BulkDeleteBrandRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteBrandOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteBrandResponseViewModel>.Success(new BulkDeleteBrandResponseViewModel(), "All Brands Deleted Successfully");
            else
                return EndPointResponse<BulkDeleteBrandResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
