using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Orders.CompletedOrder.Commands;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Orders.CompletedOrder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.CompletedOrder
{
    public class CompletedOrderEndpoint : EndpointBase<CompletedOrderRequestViewModel, CompletedOrderResponseViewModel>
    {
        public CompletedOrderEndpoint(EndpointBaseParameters<CompletedOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CompletedOrder })]
        public async Task<EndPointResponse<CompletedOrderResponseViewModel>> CompletedOrder(CompletedOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CompleteOrderOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<CompletedOrderResponseViewModel>.Success(new CompletedOrderResponseViewModel(), " Order Completed Successfully");
            else
                return EndPointResponse<CompletedOrderResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
