using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Orders.ApproveOrder.Orchistrator;

namespace KOG.ECommerce.Features.Orders.ApproveOrder
{
    public class ApproveOrderEndPoint : EndpointBase<ApproveOrderRequestViewModel, ApproveOrderResponseViewModel>
    {
        public ApproveOrderEndPoint(EndpointBaseParameters<ApproveOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ApproveOrder })]
        public async Task<EndPointResponse<ApproveOrderResponseViewModel>> ApproveOrder(ApproveOrderRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ApproveOrderOrchistrator>());
            if (result.IsSuccess)
                return EndPointResponse<ApproveOrderResponseViewModel>.Success(new ApproveOrderResponseViewModel(), " Order Confirmed Successfully");
            else
                return EndPointResponse<ApproveOrderResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
