using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Common.Categories.Queries
{
    public record GetCategoryByIdQuery(string ID):IRequestBase<GetCategoryByIdDTO>;
    public class GetCategoryByIdQueryHandler : RequestHandlerBase<Category, GetCategoryByIdQuery, GetCategoryByIdDTO>
    {
        public GetCategoryByIdQueryHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetCategoryByIdDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category=_repository.GetByID(request.ID).MapOne<GetCategoryByIdDTO>();
            if (category == null) { 
                return RequestResult<GetCategoryByIdDTO>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<GetCategoryByIdDTO>.Success(category);

        }
    }
}
