using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct
{
    public class DeactivatediscountEndPoint : EndpointBase<DeactivatediscountRequestViewModel, DeactivatediscountResponseViewModel>
    {
        public DeactivatediscountEndPoint(EndpointBaseParameters<DeactivatediscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveDiscount })]
        public async Task<EndPointResponse<DeactivatediscountResponseViewModel>> DeactiveDiscount(DeactivatediscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivatediscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivatediscountResponseViewModel>.Success(new DeactivatediscountResponseViewModel(), "Discount Deactived successfully.");
            else
                return EndPointResponse<DeactivatediscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
