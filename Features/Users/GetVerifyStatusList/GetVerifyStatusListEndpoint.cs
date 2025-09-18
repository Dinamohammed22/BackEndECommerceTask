using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Users.GetVerifyStatusList.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.GetVerifyStatusList
{
    public class GetVerifyStatusListEndpoint : EndpointBase<GetVerifyStatusListRequestViewModel, GetVerifyStatusListResponseViewModel>
    {
        public GetVerifyStatusListEndpoint(EndpointBaseParameters<GetVerifyStatusListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetVerifyStatusList})]
        public async Task<EndPointResponse<IEnumerable<GetVerifyStatusListResponseViewModel>>> SelectList()
        {

            var result = await _mediator.Send( new GetVerifyStatusListQuery());

            var response = result.Data.MapList<GetVerifyStatusListResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<GetVerifyStatusListResponseViewModel>>.Success(response, "Get all verify status list successfully.");
            else
                return EndPointResponse<IEnumerable<GetVerifyStatusListResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
