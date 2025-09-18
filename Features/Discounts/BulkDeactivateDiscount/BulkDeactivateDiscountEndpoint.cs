using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Discounts.BulkDeactivateDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.BulkDeactivateDiscount
{
    public class BulkDeactivateDiscountEndpoint : EndpointBase<BulkDeactivateDiscountRequestViewModel, BulkDeactivateDiscountResponseViewModel>
    {
        public BulkDeactivateDiscountEndpoint(EndpointBaseParameters<BulkDeactivateDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateDiscount })]
        public async Task<EndPointResponse<BulkDeactivateDiscountResponseViewModel>> DeactiveDiscount(BulkDeactivateDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateDiscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateDiscountResponseViewModel>.Success(new BulkDeactivateDiscountResponseViewModel(), "Discounts Deactivated successfully");
            else
                return EndPointResponse<BulkDeactivateDiscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
