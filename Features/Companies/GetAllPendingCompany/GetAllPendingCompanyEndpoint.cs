using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.GetAllPendingCompany
{
    public class GetAllPendingCompanyEndpoint : EndpointBase<GetAllPendingCompanyRequestViewModel, GetAllPendingCompanyResponseViewModel>
    {
        public GetAllPendingCompanyEndpoint(EndpointBaseParameters<GetAllPendingCompanyRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllPendingOrVerifiedCompany })]
        public async Task<EndPointResponse<PagingViewModel<GetAllPendingCompanyResponseViewModel>>> GetAllPendingOrVerifiedCompany([FromQuery] GetAllPendingCompanyRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetAllPendingCompanyQuery>());

            var response = result.Data.MapPage<CompanyVerifyStatusDTO, GetAllPendingCompanyResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllPendingCompanyResponseViewModel>>.Success(response, "Get Company Pending Or Verified List Successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllPendingCompanyResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
