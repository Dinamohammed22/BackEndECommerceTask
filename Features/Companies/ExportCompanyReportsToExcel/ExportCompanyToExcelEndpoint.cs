using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.ExportCompanyReportsToExcel
{
    public class ExportCompanyReportsToExcelEndpoint : EndpointBase<ExportCompanyReportsToExcelRequestViewModel, ExportCompanyReportsToExcelResponseViewModel>
    {
        public ExportCompanyReportsToExcelEndpoint(EndpointBaseParameters<ExportCompanyReportsToExcelRequestViewModel> dependencyCollection): base(dependencyCollection) { }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.ExportCompanyReportsToExcel })]
        public async Task<ActionResult<EndPointResponse<ExportCompanyReportsToExcelResponseViewModel>>> ExportCompanyReportsToExcel([FromQuery] ExportCompanyReportsToExcelRequestViewModel? filter)
        {
            var query = filter.MapOne<ExportCompanyReportsToExcelQuery>();
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

            return EndPointResponse<ExportCompanyReportsToExcelResponseViewModel>
                .Failure(ErrorCode.NotFound, "No Companis found.");
        }
    }
}
