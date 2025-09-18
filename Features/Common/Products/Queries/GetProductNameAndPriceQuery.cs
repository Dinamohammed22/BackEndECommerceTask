using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductNameAndPriceQuery(string ID):IRequestBase<GetProductNameAndPriceDTO>;
    public class GetProductNameAndPriceQueryHandler : RequestHandlerBase<Product, GetProductNameAndPriceQuery, GetProductNameAndPriceDTO>
    {
        public GetProductNameAndPriceQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetProductNameAndPriceDTO>> Handle(GetProductNameAndPriceQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.Get(p => p.ID == request.ID)
                .Include(p => p.Company)
                .FirstOrDefaultAsync(cancellationToken);

            var dto = product.MapOne<GetProductNameAndPriceDTO>();
            return RequestResult<GetProductNameAndPriceDTO>.Success(dto);

        }
    }
}
