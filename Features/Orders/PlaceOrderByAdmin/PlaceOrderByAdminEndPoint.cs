using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Orders.PlaceOrderByAdmin.Orchistrator;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByAdmin
{
    public class PlaceOrderByAdminEndPoint : EndpointBase<PlaceOrderByAdminRequestViewModel, PlaceOrderByAdminResponseViewModel>
    {
        public PlaceOrderByAdminEndPoint(EndpointBaseParameters<PlaceOrderByAdminRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.PlacedAnOrderByAdmin })]
        public async Task<EndPointResponse<PlaceOrderByAdminResponseViewModel>> PlacedAnOrderByAdmin(PlaceOrderByAdminRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<PlaceOrderByAdminOrchisterator>());

            if (result.IsSuccess)
                return EndPointResponse<PlaceOrderByAdminResponseViewModel>.Success(
                    result.Data.MapOne<PlaceOrderByAdminResponseViewModel>(), "Order Placed by admin Successfully");
            else
                return EndPointResponse<PlaceOrderByAdminResponseViewModel>.Failure(result.ErrorCode,result.Message);

        }
    }
}
