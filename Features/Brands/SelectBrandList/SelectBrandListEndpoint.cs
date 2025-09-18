using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.SelectBrandList
{
    public class SelectBrandListEndpoint : EndpointBase<SelectBrandListRequestViewModel, SelectBrandListResponseViewModel>
    {
        public SelectBrandListEndpoint(EndpointBaseParameters<SelectBrandListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectBrandList})]
        public async Task<EndPointResponse<IEnumerable<SelectBrandListResponseViewModel>>> SelectBrandList()
        {


            var result = await _mediator.Send(new SelectBrandListQuery());

            var response = result.Data.MapList<SelectBrandListResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectBrandListResponseViewModel>>.Success(response, "Brands filtered successfully.");
            else
                return EndPointResponse<IEnumerable<SelectBrandListResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
