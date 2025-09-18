using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Users.DeactivateUser
{
    public class DeactivateUserEndPoint : EndpointBase<DeactivateUserRequestViewModel, DeactivateUserResponseViewModel>
    {
        public DeactivateUserEndPoint(EndpointBaseParameters<DeactivateUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateUser })]
        public async Task<EndPointResponse<DeactivateUserResponseViewModel>> DeactivateUser(DeactivateUserRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactivateUserCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivateUserResponseViewModel>.Success(new DeactivateUserResponseViewModel(), "User Deactivated successfully.");
            else
                return EndPointResponse<DeactivateUserResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
