using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Orders.InProcessOrder.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.InProcessOrder
{
    public class InProcessOrderEndpoint : EndpointBase<InProcessOrderRequestViewModel, InProcessOrderResponseViewModel>
    {
        public InProcessOrderEndpoint(EndpointBaseParameters<InProcessOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.InProcessOrder })]
        public async Task<EndPointResponse<InProcessOrderResponseViewModel>> InProcessOrder(InProcessOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<InProcessOrderCommand>());
            if (result.IsSuccess)
                return EndPointResponse<InProcessOrderResponseViewModel>.Success(new InProcessOrderResponseViewModel(), " Order InProcess Successfully");
            else
                return EndPointResponse<InProcessOrderResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
