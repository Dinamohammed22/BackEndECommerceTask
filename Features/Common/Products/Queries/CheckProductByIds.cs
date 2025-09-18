using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Products;
using Org.BouncyCastle.Crypto;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CheckProductByIds(List<string> Ids) : IRequestBase<bool>;
    public class CheckProductByIdsGetProductByIdHandler : RequestHandlerBase<Product, CheckProductByIds, bool>
    {
        public CheckProductByIdsGetProductByIdHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(CheckProductByIds request, CancellationToken cancellationToken)
        {
            var productIds = _repository.Get(p => request.Ids.Contains(p.ID)).Select(x => x.ID).ToList();

            return RequestResult<bool>.Success(!request.Ids.Except(productIds).Any());

        }
    }
}
