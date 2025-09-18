using AutoMapper.QueryableExtensions;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetAllCategoryIndexQuery(string? CategoryId, string? SubCategoryId, int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<GetAllCategoryIndexDTO>>;

    public class GetAllCategoryIndexQueryHandler : RequestHandlerBase<Category, GetAllCategoryIndexQuery, PagingViewModel<GetAllCategoryIndexDTO>>
    {
        public GetAllCategoryIndexQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<PagingViewModel<GetAllCategoryIndexDTO>>> Handle(GetAllCategoryIndexQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Category>(true);

            predicate = predicate
                .And(c => string.IsNullOrEmpty(request.SubCategoryId) || c.ID == request.SubCategoryId)
                .And(c => string.IsNullOrEmpty(request.CategoryId) || c.ParentCategoryId == request.CategoryId || c.ID == request.CategoryId);

            var query = await _repository
                .Get(predicate)
                 .Map<GetAllCategoryIndexDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);

            foreach (var dto in query.Items)
            {
                var productCountResult = await _mediator.Send(new CountProductsByCategoryIdQuery(dto.ID));
                dto.ProductCount = productCountResult.IsSuccess ? productCountResult.Data : 0;

                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(dto.ID, SourceType.Category));
                dto.ImagePath = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;

                dto.SubCategoryCount = query.Items.Count(c => c.ParentCategoryId == dto.ID);
            }

            return RequestResult<PagingViewModel<GetAllCategoryIndexDTO>>.Success(query);
        }

    }
}
