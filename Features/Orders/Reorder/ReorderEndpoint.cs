using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Features.Orders.PlacedAnOrder;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Orders.Reorder.Orchestrators;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Orders.Reorder
{
    public class ReorderEndpoint : EndpointBase<ReorderRequestViewModel, ReorderResponseViewModel>
    {
        public ReorderEndpoint(EndpointBaseParameters<ReorderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.Reorder })]
        public async Task<EndPointResponse<ReorderResponseViewModel>> Post(ReorderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<ReorderOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<ReorderResponseViewModel>.Success(new ReorderResponseViewModel(), "Reorder Successfully");
            else
                return EndPointResponse<ReorderResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
