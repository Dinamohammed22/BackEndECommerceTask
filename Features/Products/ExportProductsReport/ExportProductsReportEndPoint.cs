using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Products.ExportProductsToExcel;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Products.ExportProductsReport
{
    public class ExportProductsReportEndPoint : EndpointBase<ExportProductsReportRequestViewModel, ExportProductsReportResponseViewModel>
    {
        public ExportProductsReportEndPoint(EndpointBaseParameters<ExportProductsReportRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportProductsReport })]
        public async Task<ActionResult<EndPointResponse<ExportProductsReportResponseViewModel>>> ExportProductsReport(
           [FromQuery] ExportProductsReportRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportProductsReportQuery>();
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

            return EndPointResponse<ExportProductsReportResponseViewModel>
                .Failure(ErrorCode.NotFound, "No Products found.");
        }
    }
}
