using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.BulkActivateCategory.Orchisterator;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Categories.BulkActivateCategory
{
    public class BulkActivateCategoryEndPoint : EndpointBase<BulkActivateCategoryRequestViewModel, BulkActivateCategoryResponseViewModel>
    {
        public BulkActivateCategoryEndPoint(EndpointBaseParameters<BulkActivateCategoryRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateCategory })]
        public async Task<EndPointResponse<BulkActivateCategoryResponseViewModel>> BulkActivateCategory(BulkActivateCategoryRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateCategoryOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateCategoryResponseViewModel>.Success(new BulkActivateCategoryResponseViewModel(), "All Categories Activated Successfully");
            else
                return EndPointResponse<BulkActivateCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
