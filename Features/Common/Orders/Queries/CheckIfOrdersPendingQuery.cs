using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record CheckIfOrdersPendingQuery(List<string> OrderIds):IRequestBase<bool>;
    public class CheckIfOrdersPendingQueryHandler : RequestHandlerBase<Order, CheckIfOrdersPendingQuery, bool>
    {
        public CheckIfOrdersPendingQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckIfOrdersPendingQuery request, CancellationToken cancellationToken)
        {
            foreach (var item in request.OrderIds)
            {
                var check = await _repository.AnyAsync(o => o.ID == item&& o.Status==OrderStatus.Pending);
                if(check)
                {
                    return RequestResult<bool>.Success(check);
                }
            }
            return RequestResult<bool>.Success(false);
        }
    }
}
