using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetSubcategoriesQuery(string CategoryId):IRequestBase<IEnumerable<SubcategoryDTO>>;
    public class GetSubcategoriesQueryHandler : RequestHandlerBase<Category, GetSubcategoriesQuery, IEnumerable<SubcategoryDTO>>
    {
        public GetSubcategoriesQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SubcategoryDTO>>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            var subcategories = await _repository
                .Get(c => c.ParentCategoryId == request.CategoryId)
                .Map<SubcategoryDTO>()
                .ToListAsync();
            if (!subcategories.Any())
            {
                return RequestResult<IEnumerable<SubcategoryDTO>>.Failure(ErrorCode.NotFound);
            }
            foreach (var subcategory in subcategories)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(subcategory.Id, SourceType.Category));
                if (mediaResult.IsSuccess)
                {
                    subcategory.Path = mediaResult.Data; 
                }
            }

            return RequestResult<IEnumerable<SubcategoryDTO>>.Success(subcategories);
        }

    }
}
