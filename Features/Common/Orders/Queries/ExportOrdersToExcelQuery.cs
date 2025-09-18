using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Orders.GetAllOrders.Orchestrator;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record ExportOrdersToExcelQuery(
        string? CustomerName, 
        string? CustomerNumber,
        string? OrderNumber,
        OrderStatus? OrderStatus,
        double? TotalPrice, 
        DateTime? From,
        DateTime? To
    ) : IRequestBase<ExportOrdersDTO>;

    public class ExportOrdersToExcelQueryHandler : RequestHandlerBase<Order, ExportOrdersToExcelQuery, ExportOrdersDTO>
    {
        public ExportOrdersToExcelQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ExportOrdersDTO>> Handle(ExportOrdersToExcelQuery request, CancellationToken cancellationToken)
        {
            var orchestratorRequest = request.MapOne<GetAllOrdersOrchestrator>() with { pageIndex = 1, pageSize = int.MaxValue };
            var orders = (await _mediator.Send(orchestratorRequest)).Data.Items.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                worksheet.Cells[1, 1].Value = "Order Number";
                worksheet.Cells[1, 2].Value = "Customer Name";
                worksheet.Cells[1, 3].Value = "Customer Number";
                worksheet.Cells[1, 4].Value = "Order Status";
                worksheet.Cells[1, 5].Value = "Total Price";
                worksheet.Cells[1, 6].Value = "Created Date";

                if (orders == null || orders.Count == 0)
                    throw new InvalidOperationException("No orders available to export.");

                for (int i = 0; i < orders.Count; i++)
                {
                    var order = orders[i];

                    worksheet.Cells[i + 2, 1].Value = order.OrderNumber ?? "N/A";
                    worksheet.Cells[i + 2, 2].Value = order.Name ?? "N/A";
                    worksheet.Cells[i + 2, 3].Value = order.Mobile ?? "N/A";
                    worksheet.Cells[i + 2, 4].Value = order.OrderStatus.ToString();
                    worksheet.Cells[i + 2, 5].Value = order.TotalPrice;
                    worksheet.Cells[i + 2, 6].Value = order.CreatedDate;
                    worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "MM/dd/yyyy";
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var fileContents = package.GetAsByteArray();
                var FileName = "Orders_Export.xlsx";
                var ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                ExportOrdersDTO ordersDTO = new ExportOrdersDTO(fileContents, FileName, ContentType);

                return RequestResult<ExportOrdersDTO>.Success(ordersDTO);
            } 
        } 
    }
}
