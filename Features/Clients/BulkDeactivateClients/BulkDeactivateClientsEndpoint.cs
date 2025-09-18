using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.BulkDeactivateClients.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.BulkDeactivateClients
{
    public class BulkDeactivateClientsEndpoint : EndpointBase<BulkDeactivateClientsRequestViewModel, BulkDeactivateClientsResponseViewModel>
    {
        public BulkDeactivateClientsEndpoint(EndpointBaseParameters<BulkDeactivateClientsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkDeactivateClients })]
        public async Task<EndPointResponse<BulkDeactivateClientsResponseViewModel>> DeactivateClients(BulkDeactivateClientsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkDeactivateClientsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<BulkDeactivateClientsResponseViewModel>.Success(new BulkDeactivateClientsResponseViewModel(), "Clients Deactivated successfully");
            else
                return EndPointResponse<BulkDeactivateClientsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
