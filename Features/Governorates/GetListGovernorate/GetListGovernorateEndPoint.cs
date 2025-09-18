using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Governorates.GetListGovernorate
{
    public class GetListGovernorateEndPoint : EndpointBase<GetListGovernorateRequestViewModel, GetListGovernorateResponseViewModel>
    {
        public GetListGovernorateEndPoint(EndpointBaseParameters<GetListGovernorateRequestViewModel> dependencyCollection) : base(dependencyCollection)
        { }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetGovernorateList })]
        public async Task<EndPointResponse<PagingViewModel<GetListGovernorateResponseViewModel>>> GetList([FromQuery] GetListGovernorateRequestViewModel viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<GetListGovernorateQuery>());

            var response = result.Data.MapPage<GovernorateProfileDTO, GetListGovernorateResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetListGovernorateResponseViewModel>>.Success(response, "Governorate filtered successfully");
            else
                return EndPointResponse<PagingViewModel<GetListGovernorateResponseViewModel>>.Failure(result.ErrorCode);

        }

    }
}
