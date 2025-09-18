using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Clients.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Clients.Queries
{
    public record ExportClientToExcelQuery(string? Name, string? Email, string? Mobile, string? NationalNumber, string? ClientGroupId,
        VerifyStatus? VerifyStatus, DateTime? From, DateTime? To, Religion? Religion)
        : IRequestBase<ExportClientsDTO>;
    public class ExportClientToExcelQueryHandler : RequestHandlerBase<Client, ExportClientToExcelQuery, ExportClientsDTO>
    {
        public ExportClientToExcelQueryHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ExportClientsDTO>> Handle(ExportClientToExcelQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Client>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                .And(c => string.IsNullOrEmpty(request.NationalNumber) || c.NationalNumber.Contains(request.NationalNumber))
                .And(c => string.IsNullOrEmpty(request.Mobile) || c.Mobile.Contains(request.Mobile) || c.Phone.Contains(request.Mobile))
                .And(c => string.IsNullOrEmpty(request.Email) || c.Email.Contains(request.Email))
                .And(c => string.IsNullOrEmpty(request.ClientGroupId) || c.ClientGroupId.Contains(request.ClientGroupId))
                .And(c => !request.VerifyStatus.HasValue || c.User.VerifyStatus == request.VerifyStatus)
                .And(c => !request.From.HasValue || c.CreatedDate >= request.From)
                .And(c => !request.To.HasValue || c.CreatedDate <= request.To).And(c => !request.Religion.HasValue || c.Religion == request.Religion);

            var clients = await _repository.Get(predicate)
                .Include(c => c.ClientGroup)
                .Include(c => c.User)
                .Select(c => new SearchClientProfileDTO
                {
                    Name = c.Name,
                    Email = c.Email,
                    Mobile = c.Mobile,
                    NationalNumber = c.NationalNumber,
                    ClientGroupName = c.ClientGroup.Name,
                    VerifyStatus = c.User.VerifyStatus,
                    ClientActivity = c.ClientActivity??ClientActivity.POS,
                    IsActive = c.User.IsActive,
                    Phone = c.Phone,
                    TotalOrders = c.Orders.Count,
                    Religion = c.Religion,
                })
                .ToListAsync();

            if (clients == null || clients.Count == 0)
                throw new InvalidOperationException("No clients available to export.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Clients");

                // Add headers
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Mobile";
                worksheet.Cells[1, 4].Value = "National Number";
                worksheet.Cells[1, 5].Value = "Client Group Name";
                worksheet.Cells[1, 6].Value = "Verify Status";
                worksheet.Cells[1, 7].Value = "Is Active";
                worksheet.Cells[1, 8].Value = "Phone";
                worksheet.Cells[1, 9].Value = "Total Orders";
                worksheet.Cells[1, 10].Value = "Client Activity";
                worksheet.Cells[1, 11].Value = "Religion";

                // Fill data
                for (int i = 0; i < clients.Count; i++)
                {
                    var client = clients[i];

                    worksheet.Cells[i + 2, 1].Value = client.Name ?? "N/A";
                    worksheet.Cells[i + 2, 2].Value = client.Email ?? "N/A";
                    worksheet.Cells[i + 2, 3].Value = client.Mobile ?? "N/A";
                    worksheet.Cells[i + 2, 4].Value = client.NationalNumber ?? "N/A";
                    worksheet.Cells[i + 2, 5].Value = client.ClientGroupName ?? "N/A";
                    worksheet.Cells[i + 2, 6].Value = client.VerifyStatus.ToString() ?? "N/A";
                    worksheet.Cells[i + 2, 7].Value = client.IsActive ? "Active" : "Inactive";
                    worksheet.Cells[i + 2, 8].Value = client.Phone ?? "N/A";
                    worksheet.Cells[i + 2, 9].Value = client.TotalOrders.ToString();
                    worksheet.Cells[i + 2, 10].Value = client.ClientActivity.ToString() ?? "N/A";
                    worksheet.Cells[i + 2, 11].Value = client.Religion.ToString() ?? "N/A";
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Cells.AutoFitColumns();

                // Convert to byte array
                var fileContents = package.GetAsByteArray();
                var fileName = "Clients_Export.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var exportClientsDTO = new ExportClientsDTO(fileContents, fileName, contentType);

                return RequestResult<ExportClientsDTO>.Success(exportClientsDTO);
            }
        }
    }
}
