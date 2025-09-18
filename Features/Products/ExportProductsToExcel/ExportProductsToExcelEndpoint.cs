using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.ExportProductsToExcel
{
    public class ExportProductsToExcelEndpoint : EndpointBase<ExportProductsToExcelRequestViewModel, ExportProductsToExcelResponseViewModel>
    {
        public ExportProductsToExcelEndpoint(EndpointBaseParameters<ExportProductsToExcelRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportProductsToExcel })]
        public async Task<ActionResult<EndPointResponse<ExportProductsToExcelResponseViewModel>>> AllProducts(
           [FromQuery] ExportProductsToExcelRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportProductsToExcelQuery>();
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                var fileResult = new FileContentResult(result.Data.FileContent, result.Data.ContentType)
                {
                    FileDownloadName = result.Data.FileName,
                    EnableRangeProcessing = false 
                };

                return fileResult;
            }

            return EndPointResponse<ExportProductsToExcelResponseViewModel>
                .Failure(ErrorCode.NotFound, "No Products found.");
        }
    }
}
