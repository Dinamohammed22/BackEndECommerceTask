using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Modules.CreateModule.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Modules.CreateModule
{
    public class CreateModuleEndPoint : EndpointBase<CreateModuleRequestViewModel, CreateModuleResponseViewModel>
    {
        public CreateModuleEndPoint(EndpointBaseParameters<CreateModuleRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateModule })]
        public async Task<EndPointResponse<CreateModuleResponseViewModel>> CreateModule(CreateModuleRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            var result = await _mediator.Send(viewModel.MapOne<CreateModuleCommand>());

            if (result.IsSuccess)
                return EndPointResponse<CreateModuleResponseViewModel>.Success(new CreateModuleResponseViewModel(), "Module Added successfully");
            else
                return EndPointResponse<CreateModuleResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
