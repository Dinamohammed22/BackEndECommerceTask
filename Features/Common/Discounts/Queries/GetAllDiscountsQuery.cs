using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    public record GetAllDiscountsQuery(int pageIndex = 1, int pageSize = 100) :IRequestBase<PagingViewModel<GetAllDiscountsDTO>>;
    public class GetAllDiscountsQueryHandler : RequestHandlerBase<Discount, GetAllDiscountsQuery, PagingViewModel<GetAllDiscountsDTO>>
    {
        public GetAllDiscountsQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllDiscountsDTO>>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discounts = await _repository.Get()
                .Map<GetAllDiscountsDTO>()
                .ToPagesAsync(request.pageIndex, request.pageSize);
            return RequestResult<PagingViewModel<GetAllDiscountsDTO>>.Success(discounts);
        }
    }
}
