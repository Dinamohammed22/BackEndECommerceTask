using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.CreateClient.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.CreateClient
{
    public class CreateClientEndPoint : EndpointBase<CreateClientRequestViewModel, CreateClientResponseViewModel>
    {
        public CreateClientEndPoint(EndpointBaseParameters<CreateClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateClient })]
        public async Task<EndPointResponse<CreateClientResponseViewModel>> Post(CreateClientRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateClientOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<CreateClientResponseViewModel>.Success(new CreateClientResponseViewModel(), "Client Added successfully.");
            }
            return EndPointResponse<CreateClientResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
