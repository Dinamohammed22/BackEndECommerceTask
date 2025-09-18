using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Features.Orders.GetOrderByID.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.GetOrderByID
{
    public class GetOrderByIDEndPoint : EndpointBase<GetOrderByIDRequestViewModel, GetOrderByIDResponseViewModel>
    {
        public GetOrderByIDEndPoint(EndpointBaseParameters<GetOrderByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetOrderByID })]
        public async Task<EndPointResponse<GetOrderByIDResponseViewModel>> GetOrderByID([FromQuery] GetOrderByIDRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<GetOrderByIDOrchestrator>());

            var response = result.Data.MapOne<GetOrderByIDResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetOrderByIDResponseViewModel>.Success(response, "Get Order successfully");
            else
                return EndPointResponse<GetOrderByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
