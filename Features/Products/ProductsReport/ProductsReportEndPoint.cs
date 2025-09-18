using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.ProductsReport
{
    public class ProductsReportEndPoint : EndpointBase<ProductsReportRequestViewModel, ProductsReportResponseViewModel>
    {
        public ProductsReportEndPoint(EndpointBaseParameters<ProductsReportRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ProductsReport })]
        public async Task<EndPointResponse<PagingViewModel<ProductsReportResponseViewModel>>> ProductsReport([FromQuery] ProductsReportRequestViewModel? viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<ProductsReportQuery>());

            var response = result.Data.MapPage<ProductsReportDTO, ProductsReportResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
            {

                return EndPointResponse<PagingViewModel<ProductsReportResponseViewModel>>
                    .Success(response, "Products filtered successfully.");
            }

            return EndPointResponse<PagingViewModel<ProductsReportResponseViewModel>>
                .Failure(ErrorCode.NotFound);

        }
    }
}
