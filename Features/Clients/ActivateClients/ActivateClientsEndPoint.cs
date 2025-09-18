using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Clients.ActivateClients.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.ActivateClients
{
    public class ActivateClientsEndPoint : EndpointBase<ActivateClientsRequestViewModel, ActivateClientsResponseViewModel>
    {
        public ActivateClientsEndPoint(EndpointBaseParameters<ActivateClientsRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActivateClients })]
        public async Task<EndPointResponse<ActivateClientsResponseViewModel>> ActiveClient(ActivateClientsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActivateClientOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<ActivateClientsResponseViewModel>.Success(new ActivateClientsResponseViewModel(), "Client Activated successfully.");
            else
                return EndPointResponse<ActivateClientsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
