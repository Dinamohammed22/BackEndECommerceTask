using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Cities.DeactiveCity.Commands;
using KOG.ECommerce.Helpers;

namespace KOG.ECommerce.Features.Cities.DeactiveCity
{
    public class DeactiveCityEndPoint : EndpointBase<DeactiveCityRequestViewModel, DeactiveCityResponseViewModel>
    {
        public DeactiveCityEndPoint(EndpointBaseParameters<DeactiveCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.DeactiveCity })]
        public async Task<EndPointResponse<DeactiveCityResponseViewModel>> Deactive(DeactiveCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);
            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<DeactiveCityCommand>());
            if (result.IsSuccess)
                return EndPointResponse<DeactiveCityResponseViewModel>.Success(new DeactiveCityResponseViewModel(), "City Deactivated Successfully");
            else
                return EndPointResponse<DeactiveCityResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
