using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.CreateGovernorate
{
    public class CreateGovernorateEndPoint : EndpointBase<CreateGovernorateRequestViewModel, CreateGovernorateResponseViewModel>
    {
        public CreateGovernorateEndPoint(EndpointBaseParameters<CreateGovernorateRequestViewModel> parameters) : base(parameters) { }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateGovernorate })]
        public async Task<EndPointResponse<CreateGovernorateResponseViewModel>> AddGovernorate(CreateGovernorateRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateGovernorateCommand>());
            var response = result.Data.MapOne<CreateGovernorateResponseViewModel>();
            if (result.IsSuccess)
                return EndPointResponse<CreateGovernorateResponseViewModel>.Success(response, "Governorate Added successfully");
            else
                return EndPointResponse<CreateGovernorateResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
