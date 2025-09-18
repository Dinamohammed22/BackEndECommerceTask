using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetUserDetails
{
    public class GetUserDetailsEndPoint : EndpointBase<GetUserDetailsRequestViewModel, GetUserDetailsResponseViewModel>
    {
        public GetUserDetailsEndPoint(EndpointBaseParameters<GetUserDetailsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientDetails })]
        public async Task<EndPointResponse<GetUserDetailsResponseViewModel>> GetClientDetails([FromQuery] GetUserDetailsRequestViewModel Request)
        {
            var result = await _mediator.Send(new GetclientDetailsQuery(Request.ID));
            var response = result.Data.MapOne<GetUserDetailsResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetUserDetailsResponseViewModel>.Success(response, "client retrived succefully");
            else
                return EndPointResponse<GetUserDetailsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
