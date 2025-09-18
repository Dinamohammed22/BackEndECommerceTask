using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record ProductsReportQuery(
                                      string? ProductName,
                                      DateTime? From,
                                      DateTime? To,
                                      int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<ProductsReportDTO>>;
    public class ProductsReportQueryHandler : RequestHandlerBase<Product, ProductsReportQuery, PagingViewModel<ProductsReportDTO>>
    {
        public ProductsReportQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<ProductsReportDTO>>> Handle(ProductsReportQuery request, CancellationToken cancellationToken)
        {
            var CompanyID = "";

            if (_userState.RoleID == Role.Company)
                CompanyID = _userState.UserID;
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Product>(true)
                .And(p => string.IsNullOrEmpty(request.ProductName) || p.Name.Contains(request.ProductName))
                .And(p => string.IsNullOrEmpty(CompanyID) || p.CompanyId == CompanyID)
                .And(p => !request.From.HasValue || p.CreatedDate >= request.From.Value)
                .And(p => !request.To.HasValue || p.CreatedDate <= request.To.Value);

            var query = await _repository.Get(predicate)
                .Include(p => p.Category)
                .ThenInclude(c => c.ParentCategory)
                .Map<ProductsReportDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);


            foreach (var dto in query.Items)
            {
                var sales = await _mediator.Send(new GetPriceAndWeightForProductQuery(dto.ID));
                dto.TotalPrice = sales.Data.TotalPrice;
                dto.TotalWeight = sales.Data.TotalLiter;
            }


            return RequestResult<PagingViewModel<ProductsReportDTO>>.Success(query);
        }
    }
}
