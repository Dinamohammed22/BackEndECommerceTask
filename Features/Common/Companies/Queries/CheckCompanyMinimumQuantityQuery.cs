using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Companies;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record CheckCompanyMinimumQuantityQuery(string ID , int ProductQuantity) : IRequestBase<bool>;
    public class CheckCompanyMinimumQuantityQueryHandler : RequestHandlerBase<Company, CheckCompanyMinimumQuantityQuery, bool>
    {
        public CheckCompanyMinimumQuantityQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(CheckCompanyMinimumQuantityQuery request, CancellationToken cancellationToken)
        {
            var company = await _repository.Get(c => c.ID == request.ID).FirstOrDefaultAsync();
            if(request.ProductQuantity < company.MinimumQuantity)
            {
                return RequestResult<bool>.Failure(ErrorCode.NotEnoughProducts, $"You must order at least {company.MinimumQuantity} products from company '{company.Name}'.");
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
