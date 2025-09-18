using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.GetAllApproveOrRejectCompany
{
    public class GetAllApproveOrRejectCompanyEndpoint : EndpointBase<GetAllApproveOrRejectCompanyRequestViewModel, GetAllApproveOrRejectCompanyResponseViewModel>
    {
        public GetAllApproveOrRejectCompanyEndpoint(EndpointBaseParameters<GetAllApproveOrRejectCompanyRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllApproveOrRejectCompany })]
        public async Task<EndPointResponse<PagingViewModel<GetAllApproveOrRejectCompanyResponseViewModel>>> GetAllApproveOrRejectCompany([FromQuery] GetAllApproveOrRejectCompanyRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<GetAllApproveOrRejectCompanyQuery>());

            var response = result.Data.MapPage<CompanyVerifyStatusDTO, GetAllApproveOrRejectCompanyResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllApproveOrRejectCompanyResponseViewModel>>.Success(response, "Get Company Approve Or Reject List Successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllApproveOrRejectCompanyResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
