using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record GetBrandByIDQuery(string ID):IRequestBase<BrandProfileDTO>;
    public class GetBrandByIDQueryHandler : RequestHandlerBase<Brand, GetBrandByIDQuery, BrandProfileDTO>
    {
        public GetBrandByIDQueryHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<BrandProfileDTO>> Handle(GetBrandByIDQuery request, CancellationToken cancellationToken)
        {
            var brand =  _repository.GetByID(request.ID).MapOne<BrandProfileDTO>();

            return RequestResult<BrandProfileDTO>.Success(brand);
        }
    }


}
