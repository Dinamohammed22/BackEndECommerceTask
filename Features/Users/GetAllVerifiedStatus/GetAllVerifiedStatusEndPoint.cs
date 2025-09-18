using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Cities.Queries.GetLisCity;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Users.DTOs;
using KOG.ECommerce.Features.Common.Users.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Users.GetAllVerifiedStatus
{
    public class GetAllVerifiedStatusEndPoint : EndpointBase<GetAllVerifiedStatusRequestViewModel, GetAllVerifiedStatusResponseViewModel>
    {
        public GetAllVerifiedStatusEndPoint(EndpointBaseParameters<GetAllVerifiedStatusRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllVerifiedStatus })]
        public async Task<EndPointResponse<PagingViewModel<GetAllVerifiedStatusResponseViewModel>>> GetList([FromQuery] GetAllVerifiedStatusRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetAllVerifiedStatusQuery>());

            var response = result.Data.MapPage< VerifiedStatusDTO, GetAllVerifiedStatusResponseViewModel >();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllVerifiedStatusResponseViewModel>>.Success(response, "get verified list successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllVerifiedStatusResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
