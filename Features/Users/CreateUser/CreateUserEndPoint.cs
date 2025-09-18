using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.CreateUser
{
    public class CreateUserEndPoint : EndpointBase<CreateUserRequestViewModel, CreateUserResponseViewModel>
    {
        public CreateUserEndPoint(EndpointBaseParameters<CreateUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateUser })]
        public async Task<EndPointResponse<CreateUserResponseViewModel>> Post(CreateUserRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateUserCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<CreateUserResponseViewModel>.Success(new CreateUserResponseViewModel(), "User Added successfully.");
            }
            return EndPointResponse<CreateUserResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
