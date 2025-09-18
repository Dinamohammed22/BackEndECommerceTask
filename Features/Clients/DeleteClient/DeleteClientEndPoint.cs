using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Features.Clients.DeleteClient.Orchestrator;

namespace KOG.ECommerce.Features.Clients.DeleteClient
{
    public class DeleteClientEndPoint : EndpointBase<DeleteClientRequestViewModel, DeleteClientResponseViewModel>
    {
        public DeleteClientEndPoint(EndpointBaseParameters<DeleteClientRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteClient })]
        public async Task<EndPointResponse<DeleteClientResponseViewModel>> DeleteClient(DeleteClientRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteClientOrchestrator>());
            if (result.IsSuccess)
            {
                return EndPointResponse<DeleteClientResponseViewModel>.Success(new DeleteClientResponseViewModel(), "Client Deleted successfully.");
            }
            return EndPointResponse<DeleteClientResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
