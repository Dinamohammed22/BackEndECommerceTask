using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductCountByCompanyIdQuery(string companyId,string? ClassificationId) :IRequestBase<int>;
    public class GetProductCountByCompanyIdQueryHandler : RequestHandlerBase<Product, GetProductCountByCompanyIdQuery, int>
    {
        public GetProductCountByCompanyIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<int>> Handle(GetProductCountByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var productCount = await _repository
                .Get(c => c.CompanyId == request.companyId&&(string.IsNullOrEmpty(request.ClassificationId) || c.Company.ClassificationId == request.ClassificationId))
                .CountAsync();
            return RequestResult<int>.Success(productCount);
        }
    }
}
