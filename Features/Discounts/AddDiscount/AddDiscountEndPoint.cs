using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Discounts.AddDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Discounts.AddDiscount
{
    public class AddDiscountEndPoint : EndpointBase<AddDiscountRequestViewModel, AddDiscountResponseViewModel>
    {
        public AddDiscountEndPoint(EndpointBaseParameters<AddDiscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddDiscount })]
        public async Task<EndPointResponse<AddDiscountResponseViewModel>> AddDiscount(AddDiscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddDiscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<AddDiscountResponseViewModel>.Success(new AddDiscountResponseViewModel(), "Discount Added To Products successfully.");
            else
                return EndPointResponse<AddDiscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
