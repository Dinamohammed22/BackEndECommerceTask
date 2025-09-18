using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.EditClient.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.EditClient
{
    public class EditClientEndpoint : EndpointBase<EditClientRequestViewModel, EditClientResponseViewModel>
    {
        public EditClientEndpoint(EndpointBaseParameters<EditClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditClient })]
        public async Task<EndPointResponse<EditClientResponseViewModel>> Put(EditClientRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<EditClientOrchestrator>());

            if (result.IsSuccess)
                return EndPointResponse<EditClientResponseViewModel>.Success(new EditClientResponseViewModel(), "Client Updated successfully");
            else
                return EndPointResponse<EditClientResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
