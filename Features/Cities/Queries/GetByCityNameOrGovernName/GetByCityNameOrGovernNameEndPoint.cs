using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Cities.Queries.GetByCityNameOrGovernName
{
    public class GetByCityNameOrGovernNameEndPoint : EndpointBase<GetByCityNameOrGovernNameRequestViewModel, GetByCityNameOrGovernNameResponseViewModel>
    {
        public GetByCityNameOrGovernNameEndPoint(EndpointBaseParameters<GetByCityNameOrGovernNameRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterCity })]
        public async Task<EndPointResponse<PagingViewModel<GetByCityNameOrGovernNameResponseViewModel>>> GetByName([FromQuery] GetByCityNameOrGovernNameRequestViewModel? viewModel)
        {
           
            var result = await _mediator.Send(viewModel.MapOne< GetByCityNameOrGovernNameQuery>());

            var response = result.Data.MapPage<CityProfileDTO, GetByCityNameOrGovernNameResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse <PagingViewModel<GetByCityNameOrGovernNameResponseViewModel>>.Success(response, "Cities filtered successfully.");
            else
                return EndPointResponse <PagingViewModel<GetByCityNameOrGovernNameResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
