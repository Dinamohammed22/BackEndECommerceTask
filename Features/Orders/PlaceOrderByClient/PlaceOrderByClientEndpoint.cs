using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Orders.PlaceOrderByClient.Orchistrator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByClient
{
    public class PlaceOrderByClientEndpoint : EndpointBase<PlaceOrderByClientRequestViewModel, PlaceOrderByClientResponseViewModel>
    {
        public PlaceOrderByClientEndpoint(EndpointBaseParameters<PlaceOrderByClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.PlacedAnOrderByClient })]
        public async Task<EndPointResponse<PlaceOrderByClientResponseViewModel>> Post(PlaceOrderByClientRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<PlaceOrderByClientOrchistrator>());

            if (result.IsSuccess)
                return EndPointResponse<PlaceOrderByClientResponseViewModel>.Success(result.Data.MapOne<PlaceOrderByClientResponseViewModel>(), "Order Placed Successfully");
            else
                return EndPointResponse<PlaceOrderByClientResponseViewModel>.Failure(result.ErrorCode,result.Message);

        }
    }
}
