using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record FilterProductsQuery(
        List<string>? BrandsId,
        List<string>? CategoriesId,
        List<string>? CompaniesId,
        List<string>? ClassificationId,
        string? ProductName,
        double? FromPrice,
        double? ToPrice,
        double? Liter,
        int PageIndex = 1,
        int PageSize = 10
    ) : IRequestBase<PagingViewModel<ProductViewDTO>>;

    public class FilterProductsQueryHandler : RequestHandlerBase<Product, FilterProductsQuery, PagingViewModel<ProductViewDTO>>
    {
        public FilterProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<ProductViewDTO>>> Handle(FilterProductsQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<PagingViewModel<ProductViewDTO>>.Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var predicate = PredicateExtensions.PredicateExtensions.Begin<Product>(true)
                .And(p => p.IsActive)
                .And(p => p.Company.IsActive)
                .And(p => p.Company.CompanyGovernorates.Any(cg => cg.GovernorateId == governorateId))
                .And(p => request.BrandsId == null || !request.BrandsId.Any() || request.BrandsId.Contains(p.BrandId))
                .And(p => request.CompaniesId == null || !request.CompaniesId.Any() || request.CompaniesId.Contains(p.CompanyId))
                .And(p => request.ClassificationId == null || !request.ClassificationId.Any() || request.ClassificationId.Contains(p.Company.ClassificationId))
                .And(p => request.CategoriesId == null || !request.CategoriesId.Any() ||
                    request.CategoriesId.Contains(p.CategoryId) ||
                    (p.Category != null && request.CategoriesId.Contains(p.Category.ParentCategoryId)))
                .And(p => !request.FromPrice.HasValue || p.Price >= request.FromPrice.Value)
                .And(p => !request.ToPrice.HasValue || p.Price <= request.ToPrice.Value)
                .And(p => string.IsNullOrEmpty(request.ProductName) ||
                    p.Name.Contains(request.ProductName) ||
                    (p.Category != null && p.Category.Name.Contains(request.ProductName)) ||
                    (p.Brand != null && p.Brand.Name.Contains(request.ProductName)))
                .And(p => !request.Liter.HasValue || p.Liter == request.Liter.Value);

            var query = await _repository.Get(predicate)
                .Include(p => p.Company)
                    .ThenInclude(c => c.CompanyGovernorates)
                .Include(p => p.Company)
                    .ThenInclude(c => c.Classification)
                .Include(p => p.Category)
                    .ThenInclude(c => c.ParentCategory)
                .Map<ProductViewDTO>()
                .ToPagesAsync(request.PageIndex, request.PageSize);

            foreach (var product in query.Items)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(product.ID, SourceType.Product));
                product.Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;
            }

            return RequestResult<PagingViewModel<ProductViewDTO>>.Success(query);
        }


    }
}
