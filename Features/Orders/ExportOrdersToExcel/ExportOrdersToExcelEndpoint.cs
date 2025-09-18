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
    public class ExportOrdersToExcelEndpoint : EndpointBase<ExportOrdersToExcelRequestViewModel, ExportOrdersToExcelResponseViewModel>
    {
        public ExportOrdersToExcelEndpoint(EndpointBaseParameters<ExportOrdersToExcelRequestViewModel> dependencyCollection): base(dependencyCollection) { }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportOrdersToExcel })]
        public async Task<ActionResult<EndPointResponse<ExportOrdersToExcelResponseViewModel>>> AllOrders([FromQuery] ExportOrdersToExcelRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportOrdersToExcelQuery>();
            var result = await _mediator.Send(query);

            if (result.IsSuccess && result.Data != null)
            {
                // Prepare the file for download
                var fileResult = new FileContentResult(result.Data.FileContent, result.Data.ContentType)
                {
                    FileDownloadName = result.Data.FileName,
                    EnableRangeProcessing = false // Optional: You can enable range processing if needed for large files
                };

                // Returning the file directly for download
                return fileResult;
            }

            return EndPointResponse<ExportOrdersToExcelResponseViewModel>
                .Failure(ErrorCode.NotFound, "No orders found.");
        }
    }
}
