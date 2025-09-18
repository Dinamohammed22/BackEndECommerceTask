using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    public record GetDiscountByIDQuery(string ID) : IRequestBase<GetAllDiscountsDTO>;
    public class GetDiscountByIDQueryHandler : RequestHandlerBase<Discount, GetDiscountByIDQuery, GetAllDiscountsDTO>
    {
        public GetDiscountByIDQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetAllDiscountsDTO>> Handle(GetDiscountByIDQuery request, CancellationToken cancellationToken)
        {
            var discount = await _repository
               .Get(c => c.ID == request.ID)
               .Map<GetAllDiscountsDTO>()
               .FirstOrDefaultAsync();

            return RequestResult<GetAllDiscountsDTO>.Success(discount);
        }
    }
}
