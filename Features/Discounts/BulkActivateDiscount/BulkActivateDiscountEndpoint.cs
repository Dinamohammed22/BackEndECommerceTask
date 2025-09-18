using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Discounts.BulkActivateDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.BulkActivateDiscount
{
    public class BulkActivateDiscountEndpoint : EndpointBase<BulkActivateDiscountRequestViewModel, BulkActivateDiscountResponseViewModel>
    {
        public BulkActivateDiscountEndpoint(EndpointBaseParameters<BulkActivateDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateDiscount })]
        public async Task<EndPointResponse<BulkActivateDiscountResponseViewModel>> ActiveDiscount(BulkActivateDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateDiscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateDiscountResponseViewModel>.Success(new BulkActivateDiscountResponseViewModel(), "Discounts Activated successfully");
            else
                return EndPointResponse<BulkActivateDiscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
