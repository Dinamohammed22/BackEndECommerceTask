using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using OfficeOpenXml;
using System.Linq.Expressions;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record ExportCompanyToExcelQuery(
       string? NID,
        DateTime? From,
        DateTime? To,
        int pageIndex = 1,
        int pageSize = 100,
        string? CityID = null,
        string? GovernorateID = null,
        string? CompanyName = null
    ) : IRequestBase<ExportCompanyDTO>;

    public class ExportCompanyToExcelQueryHandler : RequestHandlerBase<Company, ExportCompanyToExcelQuery, ExportCompanyDTO>
    {
        public ExportCompanyToExcelQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ExportCompanyDTO>> Handle(ExportCompanyToExcelQuery request, CancellationToken cancellationToken)
        {
            var CompaniesRequest = request.MapOne<CompanyFilterByNameQuery>() with { pageIndex = 1, pageSize = int.MaxValue };
            List<GetAllCompaniesDTO> Companies = (await _mediator.Send(CompaniesRequest)).Data.Items.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Company");

                worksheet.Cells[1, 1].Value = "Company Code";
                worksheet.Cells[1, 2].Value = "Company Name";
                worksheet.Cells[1, 3].Value = "Company Mobile";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "Governrate";
                worksheet.Cells[1, 6].Value = "City";
                worksheet.Cells[1, 7].Value = "Classification";
                worksheet.Cells[1, 8].Value = "Total Sales";
                worksheet.Cells[1, 9].Value = "Total Net Sales";
                worksheet.Cells[1, 10].Value = "Total Sales Weight in liter";
                worksheet.Cells[1, 11].Value = "Activation";
                worksheet.Cells[1, 12].Value = "Created Date";

                if (Companies == null || Companies.Count == 0)
                    throw new InvalidOperationException("No Company available to export.");

                for (int i = 0; i < Companies.Count; i++)
                {
                    var Company = Companies[i];

                    worksheet.Cells[i + 2, 1].Value = Company.CompanyCode ?? "N/A";
                    worksheet.Cells[i + 2, 2].Value = Company.Name ?? "N/A";
                    worksheet.Cells[i + 2, 3].Value = Company.Mobile ?? "N/A";
                    worksheet.Cells[i + 2, 4].Value = Company.Address ?? "N/A";
                    worksheet.Cells[i + 2, 5].Value = Company.GovernorateName ?? "N/A";
                    worksheet.Cells[i + 2, 6].Value = Company.CityName ?? "N/A";
                    worksheet.Cells[i + 2, 7].Value = Company.ClassificationName ?? "N/A";
                    worksheet.Cells[i + 2, 8].Value = Company.TotalPrice;
                    worksheet.Cells[i + 2, 9].Value = Company.TotalNetPrice;
                    worksheet.Cells[i + 2, 10].Value = Company.TotalLiter;
                    worksheet.Cells[i + 2, 11].Value = Company.IsActive;
                    worksheet.Cells[i + 2, 12].Value = Company.CreatedDate;
                    worksheet.Cells[i + 2, 12].Style.Numberformat.Format = "MM/dd/yyyy";
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var fileContents = package.GetAsByteArray();
                var FileName = "Company_Export.xlsx";
                var ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                ExportCompanyDTO CompanyDTO = new ExportCompanyDTO(fileContents, FileName, ContentType);

                return RequestResult<ExportCompanyDTO>.Success(CompanyDTO);
            } 
        } 
    }
}
