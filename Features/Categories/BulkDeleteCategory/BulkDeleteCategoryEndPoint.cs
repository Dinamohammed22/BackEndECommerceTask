using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Categories.BulkDeleteCategory.Orchisterator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Categories.BulkDeleteCategory
{
    public class BulkDeleteCategoryEndPoint : EndpointBase<BulkDeleteCategoryRequestViewModel, BulkDeleteCategoryResponseViewModel>
    {
        public BulkDeleteCategoryEndPoint(EndpointBaseParameters<BulkDeleteCategoryRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteCategory })]
        public async Task<EndPointResponse<BulkDeleteCategoryResponseViewModel>> BulkDeleteCategory(BulkDeleteCategoryRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteCategoryOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteCategoryResponseViewModel>.Success(new BulkDeleteCategoryResponseViewModel(), "All Categories Deleted Successfully");
            else
                return EndPointResponse<BulkDeleteCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
