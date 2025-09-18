using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Classifications.SelectListClassification;
using KOG.ECommerce.Features.Common.Classifications.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.SelectListCompany
{
    public class SelectListCompanyEndpoint : EndpointBase<SelectListCompanyRequestViewModel, SelectListCompanyResponseViewModel>
    {
        public SelectListCompanyEndpoint(EndpointBaseParameters<SelectListCompanyRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectListCompany })]
        public async Task<EndPointResponse<IEnumerable<SelectListCompanyResponseViewModel>>> SelectListCompany()
        {


            var result = await _mediator.Send(new SelectListCompanyQuery());

            var response = result.Data.MapList<SelectListCompanyResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectListCompanyResponseViewModel>>.Success(response, "Company filtered successfully.");
            else
                return EndPointResponse<IEnumerable<SelectListCompanyResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
