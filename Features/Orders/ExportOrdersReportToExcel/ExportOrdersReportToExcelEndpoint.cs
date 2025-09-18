using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Orders.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Orders.ExportOrdersToExcel
{
    public class ExportOrdersReportToExcelEndpoint : EndpointBase<ExportOrdersReportToExcelRequestViewModel, ExportOrdersReportToExcelResponseViewModel>
    {
        public ExportOrdersReportToExcelEndpoint(EndpointBaseParameters<ExportOrdersReportToExcelRequestViewModel> dependencyCollection): base(dependencyCollection) { }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportOrdersReportToExcel })]
        public async Task<ActionResult<EndPointResponse<ExportOrdersReportToExcelResponseViewModel>>> AllOrders([FromQuery] ExportOrdersReportToExcelRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportOrdersReportToExcelQuery>();
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

            return EndPointResponse<ExportOrdersReportToExcelResponseViewModel>
                .Failure(ErrorCode.NotFound, "No orders found.");
        }
    }
}
