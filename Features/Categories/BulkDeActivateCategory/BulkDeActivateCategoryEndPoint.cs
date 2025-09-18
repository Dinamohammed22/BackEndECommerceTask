using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Categories.BulkDeActivateCategory.Orchisterator;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Categories.BulkDeActivateCategory
{
   public class BulkDeActivateCategoryEndPoint : EndpointBase<BulkDeActivateCategoryRequestViewModel, BulkDeActivateCategoryResponseViewModel>
    {
        public BulkDeActivateCategoryEndPoint(EndpointBaseParameters<BulkDeActivateCategoryRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeActivateCategory })]
        public async Task<EndPointResponse<BulkDeActivateCategoryResponseViewModel>> BulkDeActivateCategory(BulkDeActivateCategoryRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeActivateCategoryOrchisterator>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeActivateCategoryResponseViewModel>.Success(new BulkDeActivateCategoryResponseViewModel(), "All Categories DeActivated Successfully");
            else
                return EndPointResponse<BulkDeActivateCategoryResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
