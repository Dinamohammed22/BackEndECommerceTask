using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.GetCityByID
{
    public class GetCityByIDEndPoint : EndpointBase<GetCityByIDRequestViewModel, GetCityByIDResponseViewModel>
    {
        public GetCityByIDEndPoint(EndpointBaseParameters<GetCityByIDRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCitybyID })]
        public async Task<EndPointResponse<GetCityByIDResponseViewModel>> GetCityByID([FromQuery] GetCityByIDRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetCityByIDQuery>());

            GetCityByIDResponseViewModel response = result.Data.MapOne<GetCityByIDResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<GetCityByIDResponseViewModel>.Success(response, "Cities filtered successfully.");
            else
                return EndPointResponse<GetCityByIDResponseViewModel>.Failure(result.ErrorCode);

        }
    }
}
