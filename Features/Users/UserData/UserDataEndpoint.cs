using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.UserData
{
    public class UserDataEndpoint : EndpointBase<UserDataRequestViewModel, UserDataResponseViewModel>
    {
        public UserDataEndpoint(EndpointBaseParameters<UserDataRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.UserData })]
        public async Task<EndPointResponse<UserDataResponseViewModel>> GetUserData()
        {

            var result = await _mediator.Send(new UserDataQuery());

            var response = result.Data.MapOne<UserDataResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<UserDataResponseViewModel>.Success(response, "Get User Data successfully.");
            else
                return EndPointResponse<UserDataResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
