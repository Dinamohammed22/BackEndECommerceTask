using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductsByBrandIdQuery(string BrandId):IRequestBase<IEnumerable<ProductViewDTO>>;
    public class GetProductsByBrandIdQueryHandler : RequestHandlerBase<Product, GetProductsByBrandIdQuery, IEnumerable<ProductViewDTO>>
    {
        public GetProductsByBrandIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetProductsByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var products=_repository.Get(c=>c.BrandId==request.BrandId).Include(p => p.Company).MapList<ProductViewDTO>();
            return RequestResult<IEnumerable<ProductViewDTO>>.Success(products);
        }
    }
}
