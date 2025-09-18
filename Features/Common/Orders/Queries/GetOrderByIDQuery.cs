using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.ClientGroups;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.ShippingAddresses;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record GetOrderByIDQuery(string ID):IRequestBase<GetOrderByIDDTO>;
    public class GetOrderByIDQueryHandler : RequestHandlerBase<Order, GetOrderByIDQuery, GetOrderByIDDTO>
    {
        public GetOrderByIDQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetOrderByIDDTO>> Handle(GetOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.Get(o => o.ID == request.ID)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return RequestResult<GetOrderByIDDTO>.Failure(ErrorCode.OrderNotFound);
            }
            var orderItemsResult = await _mediator.Send(new GetOrderItemsByOrderIDQuery(order.ID), cancellationToken);
            if (!orderItemsResult.IsSuccess)
            {
                return RequestResult<GetOrderByIDDTO>.Failure(orderItemsResult.ErrorCode);
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
               ClientActivity:ClientData.Data.ClientActivity,
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
               ShippingAddressID:order.ShippingAddressId,
               GovernorateId:shippingAddress.Data.GovernorateId,
               CityId:shippingAddress.Data.CityId,
               Street:shippingAddress.Data.Street,
               Landmark:shippingAddress.Data.Landmark,
               ShippingAddressStatus:shippingAddress.Data.Status,
               BuildingData:shippingAddress.Data.BuildingData,
               Religion:ClientData.Data.Religion,
               Latitude:shippingAddress.Data.Latitude,
               Longitude:shippingAddress.Data.Longitude,
               Items: orderItemsResult.Data.ToList()
           );

            return RequestResult<GetOrderByIDDTO>.Success(orderDTO);
        }
    }
}
