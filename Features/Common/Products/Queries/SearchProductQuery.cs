using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record SearchProductQuery(
                                      string? ProductName,
                                      string? CategoryId,
                                      string? SubcategoryId,
                                      bool? IsActive,
                                      string? CompanyId,
                                      DateTime? From,
                                      DateTime? To,
                                      int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<SearchProductProfileDTO>>;

    public class GetSearchedProductsQueryHandler : RequestHandlerBase<Product, SearchProductQuery, PagingViewModel<SearchProductProfileDTO>>
    {
        public GetSearchedProductsQueryHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters)
        {
        }
        public override async Task<RequestResult<PagingViewModel<SearchProductProfileDTO>>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {

            var CompanyID=request.CompanyId;
            var effectiveCategoryId = !string.IsNullOrEmpty(request.SubcategoryId)
                ? request.SubcategoryId
                : request.CategoryId;
            if(_userState.RoleID==Role.Company)
                CompanyID = _userState.UserID;
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Product>(true)
                .And(p => string.IsNullOrEmpty(request.ProductName) || p.Name.Contains(request.ProductName))
                .And(p => string.IsNullOrEmpty(effectiveCategoryId) || p.CategoryId == effectiveCategoryId || p.Category.ParentCategoryId == effectiveCategoryId)
                .And(p => !request.IsActive.HasValue || p.IsActive == request.IsActive)
                .And(p => string.IsNullOrEmpty(CompanyID) || p.CompanyId == CompanyID)
                .And(p => !request.From.HasValue || p.CreatedDate >= request.From.Value)
                .And(p => !request.To.HasValue || p.CreatedDate <= request.To.Value);

            var query = await _repository.Get(predicate)
                .Include(p => p.Category)
                .ThenInclude(c => c.ParentCategory)
                .Map<SearchProductProfileDTO>() 
                .ToPagesAsync(request.pageIndex, request.pageSize);

           
            foreach (var dto in query.Items)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(dto.ID, SourceType.Product));
                dto.ImagePath = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;
                var sales= await _mediator.Send(new GetPriceAndWeightForProductQuery(dto.ID));
                dto.TotalPrice = sales.Data.TotalPrice;
                dto.TotalWeight = sales.Data.TotalLiter;
            }

           
            return RequestResult<PagingViewModel<SearchProductProfileDTO>>.Success(query);
        }
    }
}
