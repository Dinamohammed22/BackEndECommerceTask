using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record GetBrandByNameQuery(string Name, int pageIndex = 1,
        int pageSize = 100) : IRequestBase<PagingViewModel<BrandProfileDTO>>;

    public class GetBrandByNameQueryHandler : RequestHandlerBase<Brand, GetBrandByNameQuery, PagingViewModel<BrandProfileDTO>>
    {
        public GetBrandByNameQueryHandler(RequestHandlerBaseParameters<Brand> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<BrandProfileDTO>>> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var brand =await _repository
                .Get(c => c.Name.Contains(request.Name))
                .Map<BrandProfileDTO>().ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<BrandProfileDTO>>.Success(brand);
        }
    }
}
