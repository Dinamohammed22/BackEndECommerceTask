using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.GetClientByMobile
{
    public class GetClientByMobileEndPoint : EndpointBase<GetClientByMobileRequestViewModel, GetClientByMobileResponseViewModel>
    {
        public GetClientByMobileEndPoint(EndpointBaseParameters<GetClientByMobileRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetClientByMobile })]
        public async Task<EndPointResponse<GetClientByMobileResponseViewModel>> GetClientByMobile([FromQuery] GetClientByMobileRequestViewModel Request)
        {
            var result = await _mediator.Send(Request.MapOne<GetClientByMobileQuery>());
            var response = result.Data.MapOne<GetClientByMobileResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<GetClientByMobileResponseViewModel>.Success(response, "client retrived succefully");
            else
                return EndPointResponse<GetClientByMobileResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
