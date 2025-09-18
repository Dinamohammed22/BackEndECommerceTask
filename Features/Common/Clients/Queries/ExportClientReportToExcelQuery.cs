using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Models.Clients;
using OfficeOpenXml;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record ExportClientReportToExcelQuery(string? Name, DateTime? From, DateTime? To) : IRequestBase<ExportClientsDTO>;

    public class ExportClientReportToExcelQueryHandler : RequestHandlerBase<Client, ExportClientReportToExcelQuery, ExportClientsDTO>
    {
        public ExportClientReportToExcelQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ExportClientsDTO>> Handle(ExportClientReportToExcelQuery request, CancellationToken cancellationToken)
        {
            var reportQuery = new ClientReportQuery(
                Name: request.Name,
                From: request.From,
                To: request.To,
                pageIndex: 1,
                pageSize: int.MaxValue
            );

            var result = await _mediator.Send(reportQuery, cancellationToken);

            if (!result.IsSuccess || result.Data == null)
                return RequestResult<ExportClientsDTO>.Failure(result.ErrorCode);

            var clientReports = result.Data.Items?.ToList() ?? new List<ClientReportDTO>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Client Report");

            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Client Activity";
            worksheet.Cells[1, 3].Value = "Mobile";
            worksheet.Cells[1, 4].Value = "Total Price";
            worksheet.Cells[1, 5].Value = "Total Liter";

            for (int i = 0; i < clientReports.Count; i++)
            {
                var item = clientReports[i];
                worksheet.Cells[i + 2, 1].Value = item.Name;
                worksheet.Cells[i + 2, 2].Value = item.ClientActivity?.ToString();
                worksheet.Cells[i + 2, 3].Value = item.Mobile;
                worksheet.Cells[i + 2, 4].Value = item.TotalPrice;
                worksheet.Cells[i + 2, 5].Value = item.TotalLiter;
            }
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            worksheet.Cells.AutoFitColumns();

            var fileContent = package.GetAsByteArray();
            var fileName = $"ClientReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var exportDto = new ExportClientsDTO(fileContent, fileName, contentType);
            return RequestResult<ExportClientsDTO>.Success(exportDto);
        }
    }
}
