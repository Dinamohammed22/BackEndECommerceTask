using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.CompanyFilterByName
{
    public class CompanyFilterByNameEndpoint : EndpointBase<CompanyFilterByNameRequestViewModel, CompanyFilterByNameResponseViewModel>
    {
        public CompanyFilterByNameEndpoint(EndpointBaseParameters<CompanyFilterByNameRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterByNameCompany })]
        public async Task<EndPointResponse<PagingViewModel<CompanyFilterByNameResponseViewModel>>> GetList([FromQuery] CompanyFilterByNameRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<CompanyFilterByNameQuery>());

            var response = result.Data.MapPage<GetAllCompaniesDTO, CompanyFilterByNameResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<PagingViewModel<CompanyFilterByNameResponseViewModel>>.Success(response, "Companies filtered successfully");
            else
                return EndPointResponse<PagingViewModel<CompanyFilterByNameResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
