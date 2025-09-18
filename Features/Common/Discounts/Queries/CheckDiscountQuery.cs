using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Discounts;
using System.Collections.Generic;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    public record CheckDiscountQuery(DateTime StartDate, DateTime EndDate):IRequestBase<IEnumerable<CheckDiscountDTO>>;
    public class CheckDiscountQueryHandler : RequestHandlerBase<Discount, CheckDiscountQuery,IEnumerable<CheckDiscountDTO>>
    {
        public CheckDiscountQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<CheckDiscountDTO>>> Handle(CheckDiscountQuery request, CancellationToken cancellationToken)
        {
            var currentDiscounts = _repository.Get(d =>d.IsActive && (d.StartDate <= request.EndDate && d.EndDate >= request.StartDate))
              .Map<CheckDiscountDTO>();
            return RequestResult< IEnumerable < CheckDiscountDTO >>.Success(currentDiscounts);
        }
    }

}