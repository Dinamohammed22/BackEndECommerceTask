using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Discounts.DeleteDiscount.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Discounts.DeleteDiscount
{
    public class DeleteDiscountEndPoint : EndpointBase<DeleteDiscountRequestViewModel, DeleteDiscountResponseViewModel>
    {
        public DeleteDiscountEndPoint(EndpointBaseParameters<DeleteDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.deleteDiscount })]
        public async Task<EndPointResponse<DeleteDiscountResponseViewModel>> deleteDiscount(DeleteDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteDiscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteDiscountResponseViewModel>.Success(new DeleteDiscountResponseViewModel(), "Delete Discount Successfully");
            else
                return EndPointResponse<DeleteDiscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
