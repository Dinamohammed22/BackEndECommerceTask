using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.GetAllUsers
{
    public class GetAllUsersEndpoint : EndpointBase<GetAllUsersRequestViewModel, GetAllUsersResponseViewModel>
    {
        public GetAllUsersEndpoint(EndpointBaseParameters<GetAllUsersRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterUsers })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<GetAllUsersResponseViewModel>>>> FilterUsers(
          [FromQuery] GetAllUsersRequestViewModel? filter)
        {

            var result = await _mediator.Send(filter.MapOne<GetAllUsersQuery>());
            var response = result.Data.MapPage<GetAllUsersDTO, GetAllUsersResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<GetAllUsersResponseViewModel>>
                    .Success(response, "Companies filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<GetAllUsersResponseViewModel>>
                .Failure(ErrorCode.NotFound);


        }

    }
}
