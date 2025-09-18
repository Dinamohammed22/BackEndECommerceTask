using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.PlacedAnOrder
{
    public class PlaceAnOrderEndpoint : EndpointBase<PlaceAnOrderRequestViewModel, PlaceAnOrderResponseViewModel>
    {
        public PlaceAnOrderEndpoint(EndpointBaseParameters<PlaceAnOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.PlacedAnOrder })]
        public async Task<EndPointResponse<PlaceAnOrderResponseViewModel>> Post(PlaceAnOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<PlaceOrderOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<PlaceAnOrderResponseViewModel>.Success(result.Data.MapOne<PlaceAnOrderResponseViewModel>(), "Order Placed Successfully");
            else
                return EndPointResponse<PlaceAnOrderResponseViewModel>.Failure(result.ErrorCode ,result.Message);

        }
    }
}
