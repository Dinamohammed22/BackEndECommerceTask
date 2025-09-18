using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.CompanyReports
{
    public class CompanyReportsEndpoint : EndpointBase<CompanyReportsRequestViewModel, CompanyReportsResponseViewModel>
    {
        public CompanyReportsEndpoint(EndpointBaseParameters<CompanyReportsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.CompanyReports })]
        public async Task<EndPointResponse<PagingViewModel<CompanyReportsResponseViewModel>>> GetList([FromQuery] CompanyReportsRequestViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<CompanyReportsQuery>());

            var response = result.Data.MapPage<CompaniesReportsDTO, CompanyReportsResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<PagingViewModel<CompanyReportsResponseViewModel>>.Success(response, "Companies Reports filtered successfully");
            else
                return EndPointResponse<PagingViewModel<CompanyReportsResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
