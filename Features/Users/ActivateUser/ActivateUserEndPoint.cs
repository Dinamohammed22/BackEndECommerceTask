using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Users.ActivateUser
{
    public class ActivateUserEndPoint : EndpointBase<ActivateUserRequestViewModel, ActivateUserResponseViewModel>
    {
        public ActivateUserEndPoint(EndpointBaseParameters<ActivateUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActivateUser })]
        public async Task<EndPointResponse<ActivateUserResponseViewModel>> ActivateUser(ActivateUserRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivateUserCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActivateUserResponseViewModel>.Success(new ActivateUserResponseViewModel(), "User Activated successfully.");
            else
                return EndPointResponse<ActivateUserResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
