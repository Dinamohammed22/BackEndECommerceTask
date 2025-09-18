using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Features.Governorates.AddRangeGovernrates.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Governorates.AddRangeGovernrates
{
    public class AddRangeGovernratesEndPoint : EndpointBase<AddRangeGovernratesRequestViewModel, AddRangeGovernratesResponseViewModel>
    {
        public AddRangeGovernratesEndPoint(EndpointBaseParameters<AddRangeGovernratesRequestViewModel> dependencyParameters) : base(dependencyParameters)
        {

        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddRangeGovernrates })]
        public async Task<EndPointResponse<AddRangeGovernratesResponseViewModel>> AddRangeGovernrates(AddRangeGovernratesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddRangeGovernratesCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<AddRangeGovernratesResponseViewModel>.Success(new AddRangeGovernratesResponseViewModel(), "Govrnrates Added successfully.");
            }
            return EndPointResponse<AddRangeGovernratesResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
