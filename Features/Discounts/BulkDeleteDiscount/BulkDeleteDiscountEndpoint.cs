using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Discounts.BulkDeleteDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.BulkDeleteDiscount
{
    public class BulkDeleteDiscountEndpoint : EndpointBase<BulkDeleteDiscountRequestViewModel, BulkDeleteDiscountResponseViewModel>
    {
        public BulkDeleteDiscountEndpoint(EndpointBaseParameters<BulkDeleteDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeleteDiscounts })]
        public async Task<EndPointResponse<BulkDeleteDiscountResponseViewModel>> BulkDeleteDiscounts(BulkDeleteDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<BulkDeleteDiscountCommand>());

            if (result.IsSuccess)
                return EndPointResponse<BulkDeleteDiscountResponseViewModel>.Success(new BulkDeleteDiscountResponseViewModel(), "Discounts Deleted successfully.");
            else
                return EndPointResponse<BulkDeleteDiscountResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
