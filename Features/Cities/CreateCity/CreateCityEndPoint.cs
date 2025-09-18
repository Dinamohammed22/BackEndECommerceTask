using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.CreateCity
{
    public class CreateCityEndPoint:EndpointBase<CreateCityRequestViewModel,CreateCityResponseViewModel>
    {
        public CreateCityEndPoint(EndpointBaseParameters<CreateCityRequestViewModel> dependencyParameters) : base(dependencyParameters)
        {

        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CreateCity })]
        public async Task<EndPointResponse<CreateCityResponseViewModel>> Post(CreateCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<CreateCityCommand>());
            var response = result.Data.MapOne<CreateCityResponseViewModel>();
            if (result.IsSuccess)
            {
                return EndPointResponse<CreateCityResponseViewModel>.Success(response, "City Added successfully.");
            }
            return EndPointResponse<CreateCityResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
