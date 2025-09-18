using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.GetAllApproveOrRejectUser
{
    public class GetAllApproveOrRejectUserEndpoint : EndpointBase<GetAllApproveOrRejectUserRequestViewModel, GetAllApproveOrRejectUserResponseViewModel>
    {
        public GetAllApproveOrRejectUserEndpoint(EndpointBaseParameters<GetAllApproveOrRejectUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllApproveOrRejectUser })]
        public async Task<EndPointResponse<PagingViewModel<GetAllApproveOrRejectUserResponseViewModel>>> GetList([FromQuery] GetAllApproveOrRejectUserRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetAllApproveOrRejectUserQuery>());

            var response = result.Data.MapPage<VerifiedStatusDTO, GetAllApproveOrRejectUserResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllApproveOrRejectUserResponseViewModel>>.Success(response, "Get Approve Or Reject List Successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllApproveOrRejectUserResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
