using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.SelectProductList
{
    public class SelectProductListEndPoint : EndpointBase<SelectProductListRequestViewModel, SelectProductListResponseViewModel>
    {
        public SelectProductListEndPoint(EndpointBaseParameters<SelectProductListRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectProductList})]
        public async Task<EndPointResponse<IEnumerable<SelectProductListResponseViewModel>>> SelectProductList()
        {


            var result = await _mediator.Send(new SelectProductListQuery());

            var response = result.Data.MapList<SelectProductListResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectProductListResponseViewModel>>.Success(response, "Products filtered successfully.");
            else
                return EndPointResponse<IEnumerable<SelectProductListResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
