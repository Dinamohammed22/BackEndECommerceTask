using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Discounts.GetDiscountByID
{
    public class GetDiscountByIDEndPoint : EndpointBase<GetDiscountByIDRequestViewModel, GetDiscountByIDResponseViewModel>
    {
        public GetDiscountByIDEndPoint(EndpointBaseParameters<GetDiscountByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetDiscountByID })]
        public async Task<EndPointResponse<GetDiscountByIDResponseViewModel>> GetDiscountByID([FromQuery] GetDiscountByIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetDiscountByIDQuery>());

            GetDiscountByIDResponseViewModel response = result.Data.MapOne<GetDiscountByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetDiscountByIDResponseViewModel>.Success(response, "Get Discount  successfully.");
            else
                return EndPointResponse<GetDiscountByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
