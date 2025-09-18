using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Orders.DeliveredOrder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.DeliveredOrder
{
    public class DeliveredOrderEndpoint : EndpointBase<DeliveredOrderRequestViewModel, DeliveredOrderResponseViewModel>
    {
        public DeliveredOrderEndpoint(EndpointBaseParameters<DeliveredOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeliveredOrder })]
        public async Task<EndPointResponse<DeliveredOrderResponseViewModel>> DeliveredOrder(DeliveredOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeliveredOrderOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<DeliveredOrderResponseViewModel>.Success(new DeliveredOrderResponseViewModel(), " Order Delivered Successfully");
            else
                return EndPointResponse<DeliveredOrderResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
