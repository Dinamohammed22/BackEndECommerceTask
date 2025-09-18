using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Orders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetAllOrdersByUserIdQuery(int pageIndex = 1, int pageSize = 100) :IRequestBase<PagingViewModel<OrdersDTO>>;
    public class GetAllOrdersByUserIdQueryHandler : RequestHandlerBase<Order, GetAllOrdersByUserIdQuery, PagingViewModel<OrdersDTO>>
    {
        public GetAllOrdersByUserIdQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<OrdersDTO>>> Handle(GetAllOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var UserID = _userState.UserID;
            var orders = await _repository.Get(o => o.ClientId == UserID)
                .Map<OrdersDTO>().ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<OrdersDTO>>.Success(orders);
        }
    }
}
