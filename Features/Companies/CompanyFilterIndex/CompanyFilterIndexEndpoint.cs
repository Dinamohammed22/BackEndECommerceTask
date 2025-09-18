using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.CompanyFilterIndex
{
    public class CompanyFilterIndexEndpoint: EndpointBase<CompanyFilterRequestViewModel, CompanyFilterResponseViewModel>
    {
        public CompanyFilterIndexEndpoint(EndpointBaseParameters<CompanyFilterRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.FilterCompany })]
        public async Task<ActionResult<EndPointResponse<PagingViewModel<CompanyFilterResponseViewModel>>>> FilterCompanies(
            [FromQuery] CompanyFilterRequestViewModel? filter)
        {
            
                var result = await _mediator.Send(filter.MapOne<FilterCompanyQuery>());
                var response = result.Data.MapPage<FilterCompanyProfileDTO, CompanyFilterResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                {

                    return EndPointResponse<PagingViewModel<CompanyFilterResponseViewModel>>
                        .Success(response, "Companies filtered successfully.");
                }

                return EndPointResponse<PagingViewModel<CompanyFilterResponseViewModel>>
                    .Failure(ErrorCode.NotFound);
            
          
        }


    }
}
