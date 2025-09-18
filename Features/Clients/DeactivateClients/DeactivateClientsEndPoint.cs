using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.DeactivateClients.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.DeactivateClients
{
    public class DeactivateClientsEndPoint : EndpointBase<DeactivateClientsRequestViewModel, DeactivateClientsResponseViewModel>
    {
        public DeactivateClientsEndPoint(EndpointBaseParameters<DeactivateClientsRequestViewModel> parameters) : base(parameters)
        {
        }


        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactivateClients })]
        public async Task<EndPointResponse<DeactivateClientsResponseViewModel>> DeactivateClient(DeactivateClientsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveClientOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<DeactivateClientsResponseViewModel>.Success(new DeactivateClientsResponseViewModel(), "Client Deactivated successfully.");
            else
                return EndPointResponse<DeactivateClientsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
