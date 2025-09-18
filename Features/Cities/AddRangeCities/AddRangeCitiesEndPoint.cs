using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Cities.AddRangeCities.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.AddRangeCities
{
    public class AddRangeCitiesEndPoint:EndpointBase<AddRangeCitiesRequestViewModel, AddRangeCitiesResponseViewModel>
    {
        public AddRangeCitiesEndPoint(EndpointBaseParameters<AddRangeCitiesRequestViewModel> dependencyParameters) : base(dependencyParameters)
        {

        }
        [HttpPost]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.AddRangeCities })]
        public async Task<EndPointResponse<AddRangeCitiesResponseViewModel>> AddRangeCities(AddRangeCitiesRequestViewModel viewModel)
        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;
            var result = await _mediator.Send(viewModel.MapOne<AddRangeCitiesCommand>());
            if(result.IsSuccess)
            {
                return EndPointResponse<AddRangeCitiesResponseViewModel>.Success(new AddRangeCitiesResponseViewModel(), "Cities Added successfully.");
            }
            return EndPointResponse<AddRangeCitiesResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
