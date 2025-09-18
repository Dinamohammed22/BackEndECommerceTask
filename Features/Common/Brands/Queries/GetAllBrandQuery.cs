using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record GetAllBrandQuery(int? NumberOfBrand):IRequestBase<IEnumerable<GetAllBrandDTO>>;
    public class GetAllBrandQueryHandler : RequestHandlerBase<Brand, GetAllBrandQuery, IEnumerable<GetAllBrandDTO>>
    {
        public GetAllBrandQueryHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetAllBrandDTO>>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            
            var Brands = await _repository.Get(p =>  p.IsActive)
             .Take(request.NumberOfBrand ?? int.MaxValue)
             .Select(x => new GetAllBrandDTO
             {
                 ID = x.ID,
                 Name = x.Name,
             }).ToListAsync();


            var BrandViewDtos = new List<GetAllBrandDTO>();
            foreach (var Brand in Brands)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(Brand.ID, SourceType.Brand));
                var productWithMedia = Brand.MapOne<GetAllBrandDTO>() with
                {
                    Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
                };
                BrandViewDtos.Add(productWithMedia);
            }
            return RequestResult<IEnumerable<GetAllBrandDTO>>.Success(BrandViewDtos);
        }
    }
}
