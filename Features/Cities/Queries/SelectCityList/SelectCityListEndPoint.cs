using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.SelectCityList
{
    public class SelectCityListEndPoint : EndpointBase<SelectCityListRequestViewModel, SelectCityListResponseViewModel>
    {
        public SelectCityListEndPoint(EndpointBaseParameters<SelectCityListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectCityList})]
        public async Task<EndPointResponse<IEnumerable<SelectCityListResponseViewModel>>> SelectCityList([FromQuery]SelectCityListRequestViewModel viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<SelectCityListQuery>());

            var response = result.Data.MapList<SelectCityListResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectCityListResponseViewModel>>.Success(response, "Cities filtered successfully.");
            else
                return EndPointResponse<IEnumerable<SelectCityListResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
