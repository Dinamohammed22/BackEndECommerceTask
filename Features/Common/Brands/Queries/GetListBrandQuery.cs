using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record GetListBrandQuery(int pageIndex = 1,int pageSize = 100) : IRequestBase<PagingViewModel<BrandProfileDTO>>;
    
        public class GetListBrandQueryHandler : RequestHandlerBase<Brand, GetListBrandQuery, PagingViewModel<BrandProfileDTO>>
        {
            public GetListBrandQueryHandler(RequestHandlerBaseParameters<Brand> parameters) : base(parameters)
            {
            }

        public async override Task<RequestResult<PagingViewModel<BrandProfileDTO>>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {

            var query =await  _repository
                .Get()
                .Map<BrandProfileDTO>()
            .ToPagesAsync(request.pageIndex, request.pageSize); 
            foreach (var dto in query.Items)
            {
             
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(dto.ID, SourceType. Brand));
                dto.Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;

               
            }
            return RequestResult<PagingViewModel<BrandProfileDTO>>.Success(query);
        }

    }

}
