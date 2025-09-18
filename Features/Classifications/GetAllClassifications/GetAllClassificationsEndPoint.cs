using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Classifications.DTOs;
using KOG.ECommerce.Features.Common.Classifications.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.GetAllClassifications
{
    public class GetAllClassificationsEndPoint : EndpointBase<GetAllClassificationsRequestViewModel, GetAllClassificationsResponseViewModel>
    {
        public GetAllClassificationsEndPoint(EndpointBaseParameters<GetAllClassificationsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllClassifications })]
        public async Task<EndPointResponse<PagingViewModel<GetAllClassificationsResponseViewModel>>> GetAllClassifications([FromQuery] GetAllClassificationsRequestViewModel? viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<GetAllClassificationsQuery>());

            var response = result.Data.MapPage<GetAllClassificationsDTO, GetAllClassificationsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllClassificationsResponseViewModel>>.Success(response, "Classifications filtered successfully");
            else
                return EndPointResponse<PagingViewModel<GetAllClassificationsResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
