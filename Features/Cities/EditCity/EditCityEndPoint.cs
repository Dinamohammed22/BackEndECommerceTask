using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.EditCity.Commands;
using KOG.ECommerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Cities.EditCity
{
    public class EditCityEndPoint : EndpointBase<EditCityRequestViewModel, EditCityResponseViewModel>
    {
        public EditCityEndPoint(EndpointBaseParameters<EditCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpPut]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.EditCity })]
        public async Task<EndPointResponse<EditCityResponseViewModel>> Post(EditCityRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<EditCityCommand>());
            if (result.IsSuccess)
            {
                return EndPointResponse<EditCityResponseViewModel>.Success(new EditCityResponseViewModel(), "City Updated successfully.");
            }
            return EndPointResponse<EditCityResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
