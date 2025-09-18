using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Clients.ExportClientReportToExcel
{
    public class ExportClientReportToExcelEndpoint : EndpointBase<ExportClientReportToExcelRequestViewModel, ExportClientReportToExcelResponseViewModel>
    {
        public ExportClientReportToExcelEndpoint(EndpointBaseParameters<ExportClientReportToExcelRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportClientReportToExcel })]
        public async Task<ActionResult<EndPointResponse<ExportClientReportToExcelResponseViewModel>>> ExportClientReportToExcel([FromQuery] ExportClientReportToExcelRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportClientReportToExcelQuery>();
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

            return EndPointResponse<ExportClientReportToExcelResponseViewModel>
                .Failure(ErrorCode.NotFound, "No Clients found.");
        }
    }
}
