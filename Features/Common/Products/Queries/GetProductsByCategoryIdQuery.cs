using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Products;
using System.Collections.Generic;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductsByCategoryIdQuery(string CategoryId, int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<GetProductsByCategoryIdDTO>>;

    public class GetProductsByCategoryIdQueryHandler : RequestHandlerBase<Product, GetProductsByCategoryIdQuery, PagingViewModel<GetProductsByCategoryIdDTO>>
    {
        public GetProductsByCategoryIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override  Task<RequestResult<PagingViewModel<GetProductsByCategoryIdDTO>>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var Products = await _repository.Get(p => p.CategoryId == request.CategoryId ).Map<GetProductsByCategoryIdDTO>().ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<GetProductsByCategoryIdDTO>>.Success(Products);
        }
    }
}
