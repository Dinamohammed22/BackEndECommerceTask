using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.GetAllGovernorateWithAllCities
{
    public class GetAllGovernorateWithAllCitiesEndPoint : EndpointBase<GetAllGovernorateWithAllCitiesRequestViewModel, GetAllGovernorateWithAllCitiesResponseViewModel>
    {
        public GetAllGovernorateWithAllCitiesEndPoint(EndpointBaseParameters<GetAllGovernorateWithAllCitiesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllGovernorateWithAllCities })]
        public async Task<EndPointResponse<PagingViewModel<GetAllGovernorateWithAllCitiesResponseViewModel>>> GetList([FromQuery] GetAllGovernorateWithAllCitiesRequestViewModel? viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<GetAllGovernorateWithAllCitiesQuery>());

            var response = result.Data.MapPage<GovernorateWithAllCitiesProfileDTO, GetAllGovernorateWithAllCitiesResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllGovernorateWithAllCitiesResponseViewModel>>.Success(response, "Governorate filtered successfully");
            else
                return EndPointResponse<PagingViewModel<GetAllGovernorateWithAllCitiesResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
