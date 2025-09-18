using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Orders.GetOrderByID.Orchestrators
{
    public record GetOrderByIDOrchestrator(string ID) : IRequestBase<GetOrderByIDDTO>;
    public class GetOrderByIDOrchestratorHandler : RequestHandlerBase<Order, GetOrderByIDOrchestrator, GetOrderByIDDTO>
    {
        public GetOrderByIDOrchestratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderByIDDTO>> Handle(GetOrderByIDOrchestrator request, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(o => o.ID == request.ID)
                            .FirstOrDefaultAsync();

            if (order == null)
            {
                return RequestResult<GetOrderByIDDTO>.Failure(ErrorCode.OrderNotFound);
            }
            var role = _userState.RoleID;
            RequestResult<IEnumerable<OrderItemWithItemNameDTO>> orderItemsResult;
            if (role == Role.Company)
            {
                orderItemsResult = await _mediator.Send(new GetCompanyOrderItemsByOrderIDQuery(order.ID));
                if (!orderItemsResult.IsSuccess)
                {
                    return RequestResult<GetOrderByIDDTO>.Failure(orderItemsResult.ErrorCode);
                }
            }
            else
            {
                 orderItemsResult = await _mediator.Send(new GetOrderItemsByOrderIDQuery(order.ID));
                if (!orderItemsResult.IsSuccess)
                {
                    return RequestResult<GetOrderByIDDTO>.Failure(orderItemsResult.ErrorCode);
                }
            }
            var ClientData = await _mediator.Send(new GetClientByIdQuery(order.ClientId));
            if (!ClientData.IsSuccess)
            {
                return RequestResult<GetOrderByIDDTO>.Failure(ClientData.ErrorCode);
            }
            var shippingAddress = await _mediator.Send(new GetShippingAddresseIdQuery(order.ShippingAddressId));
            var orderDTO = new GetOrderByIDDTO(
               OrderNumber: order.OrderNumber,
               OrderID: order.ID,
               ClientActivity: ClientData.Data.ClientActivity,
               TotalLiter: order.TotalLiter,
               Comment: order.Comment,
               Status: order.Status,
               ClientID: order.ClientId,
               NationalNumber: ClientData.Data.NationalNumber,
               Name: ClientData.Data.Name,
               Mobile: ClientData.Data.Mobile,
               Email: ClientData.Data.Email,
               ClientGroupId: ClientData.Data.ClientGroupId,
               Phone: ClientData.Data.Phone,
               ShippingAddressID: order.ShippingAddressId,
               GovernorateId: shippingAddress.Data.GovernorateId,
               CityId: shippingAddress.Data.CityId,
               Street: shippingAddress.Data.Street,
               Landmark: shippingAddress.Data.Landmark,
               ShippingAddressStatus: shippingAddress.Data.Status,
               BuildingData: shippingAddress.Data.BuildingData,
               Religion: ClientData.Data.Religion,
               Latitude: shippingAddress.Data.Latitude,
               Longitude: shippingAddress.Data.Longitude,
               Items: orderItemsResult.Data.ToList()
           );

            return RequestResult<GetOrderByIDDTO>.Success(orderDTO);
        }
    }
}
