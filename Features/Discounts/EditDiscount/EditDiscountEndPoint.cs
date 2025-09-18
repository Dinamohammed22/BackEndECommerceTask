using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Discounts.EditDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.EditDiscount
{
    public class EditDiscountEndPoint : EndpointBase<EditDiscountRequestViewModel, EditDiscountResponseViewModel>
    {
        public EditDiscountEndPoint(EndpointBaseParameters<EditDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditDiscount })]
        public async Task<EndPointResponse<EditDiscountResponseViewModel>> EditDiscount(EditDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<EditDiscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<EditDiscountResponseViewModel>.Success(new EditDiscountResponseViewModel(), "Discount Updated successfully.");
            else
                return EndPointResponse<EditDiscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
