using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Orders.EditOrder.Orchisterator;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Orders.EditOrder
{
    public class EditOrderEndPoint : EndpointBase<EditOrderRequestViewModel, EditOrderResponseViewModel>
    {
        public EditOrderEndPoint(EndpointBaseParameters<EditOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditOrder })]
        public async Task<EndPointResponse<EditOrderResponseViewModel>> EditOrder(EditOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditOrderOrchisterator>());

            if (result.IsSuccess)
                return EndPointResponse<EditOrderResponseViewModel>.Success(new EditOrderResponseViewModel(), "Order Updated successfully");
            else
                return EndPointResponse<EditOrderResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
