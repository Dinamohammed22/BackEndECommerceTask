using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrderHistoryQuery(int pageIndex = 1, int pageSize = 100) :IRequestBase<PagingViewModel<GetOrderHistoryDTO>>;
    public class GetOrderHistoryQueryHandler : RequestHandlerBase<Order, GetOrderHistoryQuery, PagingViewModel<GetOrderHistoryDTO>>
    {
        public GetOrderHistoryQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetOrderHistoryDTO>>> Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            var UserID = _userState.UserID;

            var orders = await _repository.Get(o => o.ClientId == UserID)
                .Include(o => o.Client)
                .Select(o => new GetOrderHistoryDTO
                {
                    ID = o.ID,
                    OrderNumber = o.OrderNumber,
                    Name = o.Client.Name ?? string.Empty,
                    Mobile = o.Client.Mobile ?? string.Empty,
                    Status = o.Status,
                    TotalNetPrice = o.TotalNetPrice,
                    TotalPrice = o.TotalPrice,
                    TotalQuantity = o.TotalQuantity,
                    Date = o.Status == OrderStatus.InProcess ? (o.InProcessDate ?? o.CreatedDate) :
                                  o.Status == OrderStatus.Confirmed ? (o.ConfirmationDate ?? o.CreatedDate) :
                                  o.Status == OrderStatus.Cancelled ? (o.CancellationDate ?? o.CreatedDate) :
                                  o.Status == OrderStatus.Delivered ? (o.DeliveryDate ?? o.CreatedDate) :
                                  o.Status == OrderStatus.Completed ? (o.CompletedDate ?? o.CreatedDate) :
                                  o.CreatedDate
                })
                .ToPagesAsync(request.pageIndex, request.pageSize);

            return RequestResult<PagingViewModel<GetOrderHistoryDTO>>.Success(orders);
        }

    }
}
