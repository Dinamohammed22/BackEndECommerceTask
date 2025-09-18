using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.GetUserByID
{
    public class GetUserByIDEndPoint : EndpointBase<GetUserByIDRequestViewModel, GetUserByIDResponseViewModel>
    {
        public GetUserByIDEndPoint(EndpointBaseParameters<GetUserByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetUserById })]
        public async Task<EndPointResponse<GetUserByIDResponseViewModel>> GetUserById([FromQuery] GetUserByIDRequestViewModel Request)
        {
            var result = await _mediator.Send(new GetUserByIDQuery(Request.ID));
            var response = result.Data.MapOne<GetUserByIDResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetUserByIDResponseViewModel>.Success(response, "user retrived succefully");
            else
                return EndPointResponse<GetUserByIDResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
