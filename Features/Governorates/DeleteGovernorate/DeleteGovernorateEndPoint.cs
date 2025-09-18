using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.DeleteGovernorate
{
    public class DeleteGovernorateEndPoint : EndpointBase<DeleteGovernorateRequestViewModel, DeleteGovernorateResponseViewModel>
    {
        public DeleteGovernorateEndPoint(EndpointBaseParameters<DeleteGovernorateRequestViewModel> parameters) : base(parameters) { }
        [HttpDelete]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeleteGovernorate })]
        public async Task<EndPointResponse<DeleteGovernorateResponseViewModel>> DeleteGovernorate(DeleteGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeleteGovernorateOrchestrator>());
            if (result.IsSuccess)
                return EndPointResponse<DeleteGovernorateResponseViewModel>.Success(new DeleteGovernorateResponseViewModel(), "Governorate Deleted successfully");
            else
                return EndPointResponse<DeleteGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
