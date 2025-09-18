using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Governorates.ActiveGovernorate
{
    public class ActiveGovernorateEndpoint : EndpointBase<ActiveGovernorateRequestViewModel, ActiveGovernorateResponseViewModel>
    {
        public ActiveGovernorateEndpoint(EndpointBaseParameters<ActiveGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ActiveGovernorate })]
        public async Task<EndPointResponse<ActiveGovernorateResponseViewModel>> Active(ActiveGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<ActiveGovernorateCommand>());
            if (result.IsSuccess)
                return EndPointResponse<ActiveGovernorateResponseViewModel>.Success(new ActiveGovernorateResponseViewModel(), "Governorate Activated Successfully");
            else
                return EndPointResponse<ActiveGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
