using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record ExportProductsToExcelQuery(string? ProductName, string? CategoryId, string? SubcategoryId, bool? IsActive, string? CompanyId, DateTime? From,
                                      DateTime? To) : IRequestBase<ExportProductsDTO>;
    public class ExportProductsToExcelQueryHandler : RequestHandlerBase<Product, ExportProductsToExcelQuery, ExportProductsDTO>
    {
        public ExportProductsToExcelQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<ExportProductsDTO>> Handle(ExportProductsToExcelQuery request, CancellationToken cancellationToken)
        {
            var CompanyID = request.CompanyId;
            var effectiveCategoryId = !string.IsNullOrEmpty(request.SubcategoryId)
                ? request.SubcategoryId
                : request.CategoryId;
                if (_userState.RoleID == Role.Company)
                CompanyID = _userState.UserID;

            var predicate = PredicateExtensions.PredicateExtensions.Begin<Product>(true)
                .And(p => string.IsNullOrEmpty(request.ProductName) || p.Name.Contains(request.ProductName))
                .And(p => string.IsNullOrEmpty(effectiveCategoryId) || p.CategoryId == effectiveCategoryId || p.Category.ParentCategoryId == effectiveCategoryId)
                .And(p => !request.IsActive.HasValue || p.IsActive == request.IsActive)
                .And(p => string.IsNullOrEmpty(request.CompanyId) || p.CompanyId==request.CompanyId)
                .And(p => !request.From.HasValue || p.CreatedDate >= request.From.Value)
                .And(p => !request.To.HasValue || p.CreatedDate <= request.To.Value);
            var productEntities = await _repository.Get(predicate)
               .Include(p => p.Company)
               .Include(p => p.Category)
               .ThenInclude(c => c.ParentCategory)
               .Include(p => p.OrderItems)
               .ToListAsync();

            var products = productEntities.Select(p => new SearchProductProfileDTO
            {
                ID = p.ID,
                ProductName = p.Name,
                CategoryName = p.Category?.ParentCategory != null ? p.Category.ParentCategory.Name : p.Category?.Name,
                CompanyName = p.Company?.Name ?? "",
                SubcategoryName = p.Category?.ParentCategory != null ? p.Category?.Name : null,
                Price = p.Price,
                Quantity = p.Quantity,
                IsActive = p.IsActive,
                IsActivePoint = p.IsActivePoint,
                NumberOfPoints = p.NumberOfPoints,
                CompanyId = p.CompanyId,
                Grade = p.Grade,
                TotalPrice = p.OrderItems.Sum(oi => oi.Price),
                TotalWeight = p.OrderItems.Sum(oi => oi.Liter)
            }).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Define headers
                var headers = new[]
                 {
                   "Product Name", "Company Name", "Category Name", "Subcategory Name", "Price", "Quantity",
                   "Is Active", "Number Of Points", "Is Active Point", "Total Price", "Total Weight"
                 };
                for (var col = 0; col < headers.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = headers[col];
                }

                if (products == null || products.Count == 0)
                    throw new InvalidOperationException("No products available to export.");

                // Add product data
                for (var i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    worksheet.Cells[i + 2, 1].Value = product.ProductName ?? "N/A";
                    worksheet.Cells[i + 2, 2].Value = product.CompanyName ?? "N/A";
                    worksheet.Cells[i + 2, 3].Value = product.CategoryName ?? "N/A";
                    worksheet.Cells[i + 2, 4].Value = product.SubcategoryName ?? "N/A";
                    worksheet.Cells[i + 2, 5].Value = product.Price;
                    worksheet.Cells[i + 2, 6].Value = product.Quantity;
                    worksheet.Cells[i + 2, 7].Value = product.IsActive;
                    worksheet.Cells[i + 2, 8].Value = product.NumberOfPoints;
                    worksheet.Cells[i + 2, 9].Value = product.IsActivePoint;
                    worksheet.Cells[i + 2, 10].Value = product.TotalPrice ?? 0;
                    worksheet.Cells[i + 2, 11].Value = product.TotalWeight ?? 0;

                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // Convert the Excel package to a byte array
                var fileContents = package.GetAsByteArray();
                var fileName = "Products_Export.xlsx";
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Create ExportProductsDTO
                var exportProductsDTO = new ExportProductsDTO(fileContents, fileName, contentType);

                return RequestResult<ExportProductsDTO>.Success(exportProductsDTO);
            }
        }
    }
}
