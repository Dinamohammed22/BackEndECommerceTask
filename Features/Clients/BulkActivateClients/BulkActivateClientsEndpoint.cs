using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Clients.BulkActivateClients.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Clients.BulkActivateClients
{
    public class BulkActivateClientsEndpoint : EndpointBase<BulkActivateClientsRequestViewModel, BulkActivateClientsResponseViewModel>
    {
        public BulkActivateClientsEndpoint(EndpointBaseParameters<BulkActivateClientsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.BulkActivateClients })]
        public async Task<EndPointResponse<BulkActivateClientsResponseViewModel>> ActivateClients(BulkActivateClientsRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<BulkActivateClientsCommand>());
            if (result.IsSuccess)
                return EndPointResponse<BulkActivateClientsResponseViewModel>.Success(new BulkActivateClientsResponseViewModel(), "Clients Activated successfully");
            else
                return EndPointResponse<BulkActivateClientsResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
