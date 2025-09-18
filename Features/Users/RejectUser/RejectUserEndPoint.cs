using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.RejectUser.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Users.RejectUser
{
    public class RejectUserEndPoint : EndpointBase<RejectUserRequestViewModel, RejectUserResponseViewModel>
    {
        public RejectUserEndPoint(EndpointBaseParameters<RejectUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.RejectUser })]
        public async Task<EndPointResponse<RejectUserResponseViewModel>> Put(RejectUserRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<RejectUserCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<RejectUserResponseViewModel>.Success(new RejectUserResponseViewModel(), "User Rejected successfully");
            }
            return EndPointResponse<RejectUserResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
