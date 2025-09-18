using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CheckIfCompanyHasProductsQuery(string CompanyId):IRequestBase<bool>;
    public class CheckIfCompanyHasProductsQueryHandler : RequestHandlerBase<Product, CheckIfCompanyHasProductsQuery, bool>
    {
        public CheckIfCompanyHasProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckIfCompanyHasProductsQuery request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(p => p.CompanyId == request.CompanyId);
            return RequestResult<bool>.Success(check);
        }
    }
}
