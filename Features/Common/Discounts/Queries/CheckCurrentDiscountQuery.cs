using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Discounts;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    public record CheckCurrentDiscountQuery() : IRequestBase<GetAllDiscountsDTO>;
    public class CheckCurrentDiscountQueryHandler : RequestHandlerBase<Discount, CheckCurrentDiscountQuery, GetAllDiscountsDTO>
    {
        public CheckCurrentDiscountQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetAllDiscountsDTO>> Handle(CheckCurrentDiscountQuery request, CancellationToken cancellationToken)
        {
            var currentDiscount = _repository.Get(d => d.IsActive && (d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)).FirstOrDefault()
             .MapOne<GetAllDiscountsDTO>();
            return RequestResult<GetAllDiscountsDTO>.Success(currentDiscount);
        }
    }

}
