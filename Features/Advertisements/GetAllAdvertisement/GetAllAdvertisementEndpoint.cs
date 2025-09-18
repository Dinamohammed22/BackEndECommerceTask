using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Features.Common.Advertisements.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Advertisements.GetAllAdvertisement
{
    public class GetAllAdvertisementEndpoint : EndpointBase<GetAllAdvertisementRequestViewModel, GetAllAdvertisementResponseViewModel>
    {
        public GetAllAdvertisementEndpoint(EndpointBaseParameters<GetAllAdvertisementRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllAdvertisement })]
        public async Task<EndPointResponse<PagingViewModel<GetAllAdvertisementResponseViewModel>>> GetList([FromQuery] GetAllAdvertisementRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetAllAdvertisementQuery>());

            var response = result.Data.MapPage<GetAllAdvertisementDTO, GetAllAdvertisementResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllAdvertisementResponseViewModel>>.Success(response, "Get All Advertisement successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllAdvertisementResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
