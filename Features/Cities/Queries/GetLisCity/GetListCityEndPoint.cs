using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.GetLisCity
{
    public class GetListCityEndPoint : EndpointBase<GetListCityRequestViewModel, GetListCityResponseViewModel>
    {
        public GetListCityEndPoint(EndpointBaseParameters<GetListCityRequestViewModel> dependencyCollection) : base(dependencyCollection)
        { }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCityList })]
        public async Task<EndPointResponse<PagingViewModel<GetListCityResponseViewModel>>> GetList([FromQuery] GetListCityRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetListCityQuery>());
            
            var response = result.Data.MapPage<CityProfileDTO, GetListCityResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetListCityResponseViewModel>>.Success(response, "Cities filtered successfully.");
            else
                return EndPointResponse<PagingViewModel<GetListCityResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
