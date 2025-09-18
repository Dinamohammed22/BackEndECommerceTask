using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public class GetProductQuery : IRequestBase<IQueryable<ProductProfileDTO>>
    {
        public class GetListProductQueryHandler : RequestHandlerBase<Product, GetProductQuery, IQueryable<ProductProfileDTO>>
        {
            public GetListProductQueryHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters)
            {
            }

            public override async Task<RequestResult<IQueryable<ProductProfileDTO>>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {

                var products = _repository.Get().Map<ProductProfileDTO>();

                return RequestResult<IQueryable<ProductProfileDTO>>.Success(products);
            }
        }
    }
}
