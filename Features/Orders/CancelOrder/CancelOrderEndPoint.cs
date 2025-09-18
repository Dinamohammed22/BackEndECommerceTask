using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Orders.CancelOrder.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.CancelOrder
{
    public class CancelOrderEndPoint : EndpointBase<CancelOrderRequestViewModel, CancelOrderResponseViewModel>
    {
        public CancelOrderEndPoint(EndpointBaseParameters<CancelOrderRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CancelOrder })]
        public async Task<EndPointResponse<CancelOrderResponseViewModel>> CancelOrder(CancelOrderRequestViewModel request)
        {
            var validationResult = await ValidateRequestAsync(request);
            if (!validationResult.IsSuccess)
                return validationResult;
            
            var result = await _mediator.Send(request.MapOne<CancelOrderCommand>());
            if (result.IsSuccess)
                return EndPointResponse<CancelOrderResponseViewModel>.Success(new CancelOrderResponseViewModel() , "Your Order Canceled Succefully");
            else
                return EndPointResponse<CancelOrderResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
