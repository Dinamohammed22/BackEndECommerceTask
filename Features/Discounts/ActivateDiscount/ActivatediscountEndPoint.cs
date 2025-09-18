using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Discounts.ActivateDiscount.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Discounts.ActivateDiscount
{
    public class ActivatediscountEndPoint : EndpointBase<ActivatediscountRequestViewModel, ActivatediscountResponseViewModel>
    {
        public ActivatediscountEndPoint(EndpointBaseParameters<ActivatediscountRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveDiscount })]
        public async Task<EndPointResponse<ActivatediscountResponseViewModel>> ActiveDiscount(ActivatediscountRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivatediscountCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActivatediscountResponseViewModel>.Success(new ActivatediscountResponseViewModel(), "Discount Activated successfully");
            else
                return EndPointResponse<ActivatediscountResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
