using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Governorates.UpdateGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.UpdateGovernorate
{
    public class UpdateGovernorateEndPoint : EndpointBase<UpdateGovernorateRequestViewModel, UpdateGovernorateResponseViewModel>
    {
        public UpdateGovernorateEndPoint(EndpointBaseParameters<UpdateGovernorateRequestViewModel> parameters) : base(parameters) { }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditGovernorate })]
        public async Task<EndPointResponse<UpdateGovernorateResponseViewModel>> UpdateGovernorate(UpdateGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<UpdateGovernorateCommand>());
            if (result.IsSuccess)
                return EndPointResponse<UpdateGovernorateResponseViewModel>.Success(new UpdateGovernorateResponseViewModel(), "Governorate Updated successfully");
            else
                return EndPointResponse<UpdateGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
