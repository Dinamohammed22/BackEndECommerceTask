using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.ClientGroups.CreateClientGroup.Commands;

namespace KOG.ECommerce.Features.ClientGroups.CreateClientGroup
{
    public class CreateClientGroupEndPoint : EndpointBase<CreateClientGroupRequestViewModel, CreateClientGroupResponseViewModel>
    {
        public CreateClientGroupEndPoint(EndpointBaseParameters<CreateClientGroupRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateClientGroups })]
        public async Task<EndPointResponse<CreateClientGroupResponseViewModel>> Post(CreateClientGroupRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<CreateClientGroupCommand>());

            if (result.IsSuccess)
                return EndPointResponse<CreateClientGroupResponseViewModel>.Success(new CreateClientGroupResponseViewModel(), "ClientGroup Added successfully.");
            else
                return EndPointResponse<CreateClientGroupResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
