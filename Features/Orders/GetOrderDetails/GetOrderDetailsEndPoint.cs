using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrderDetails
{
    public class GetOrderDetailsEndPoint : EndpointBase<GetOrderDetailsRequestViewModel, GetOrderDetailsResponseViewModel>
    {
        public GetOrderDetailsEndPoint(EndpointBaseParameters<GetOrderDetailsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetOrderDetails })]
        public async Task<EndPointResponse<GetOrderDetailsResponseViewModel>> GetOrderDetails([FromQuery] GetOrderDetailsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetOrderDetailsQuery>());

            var response = result.Data.MapOne<GetOrderDetailsResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetOrderDetailsResponseViewModel>.Success(response, "Get Order successfully");
            else
                return EndPointResponse<GetOrderDetailsResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
