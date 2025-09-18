using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.EditClient.Orchestrators;
using KOG.ECommerce.Features.Clients.EditClient;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Common.Users.EditUser.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.EditClient
{
    public class EditUserEndPoint : EndpointBase<EditUserRequestViewModel, EditUserResponseViewModel>
    {
        public EditUserEndPoint(EndpointBaseParameters<EditUserRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditUser })]
        public async Task<EndPointResponse<EditUserResponseViewModel>> Put(EditUserRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditUserCommand>());

            if (result.IsSuccess)
                return EndPointResponse<EditUserResponseViewModel>.Success(new EditUserResponseViewModel(), "User Updated successfully");
            else
                return EndPointResponse<EditUserResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
