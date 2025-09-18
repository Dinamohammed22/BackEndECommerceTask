using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.EditClient.Orchestrators;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Commands;
using KOG.ECommerce.Features.Orders.EditOrder.Commands;
using KOG.ECommerce.Features.ShippingAddresses.EditShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Orders.EditOrder.Orchisterator
{
    public record EditOrderOrchisterator(
        string OrderID,
        string OrderNumber,
        List<EditOrderDTO> Items,
        OrderStatus Status,
        string? Comment,
        string ClientID,
        string? NationalNumber, 
        string Name,
        string Mobile,
        string? Email, 
        string? ClientGroupId,
        string? Phone,
        ClientActivity? ClientActivity,
        string? ShippingAddressID,
        string GovernorateId,
        string CityId,
        string Street,
        string Landmark
        , string BuildingData,
        double Latitude = 0,
        double Longitude = 0
    ) : IRequestBase<bool>;

    public class EditOrderOrchisteratorHandler : RequestHandlerBase<Order, EditOrderOrchisterator, bool>
    {
        public EditOrderOrchisteratorHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<bool>> Handle(EditOrderOrchisterator request, CancellationToken cancellationToken)
        {
           //client
            var CheckUser = await _mediator.Send(new EditClientOrchestrator(
                request.ClientID,
                request.NationalNumber,
                request.Name,
                request.Mobile,
                request.Email,
                request.ClientGroupId,
                request.Phone,
                request.ClientActivity
            ));

            if (!CheckUser.Data)
            {
                return RequestResult<bool>.Failure(CheckUser.ErrorCode);
            }

            //shipping address

            var shippingAddressId = request.ShippingAddressID;
            
            if (request.ShippingAddressID.IsNullOrEmpty())
            {
                var shippingAddress = await _mediator.Send(request.MapOne<CreateShippingAddressInOrderCommand>());
                shippingAddressId = shippingAddress.Data;
            }
            else
            {
                var CheckShippingAdress = await _mediator.Send(new EditShippingAddressCommand(
                    request.ShippingAddressID,
                    request.GovernorateId,
                    request.CityId,
                    request.Street,
                    request.Landmark,
                    request.Latitude,
                    request.Longitude,
                    request.BuildingData
                ));

                if (!CheckShippingAdress.Data)
                {
                    return RequestResult<bool>.Failure(CheckShippingAdress.ErrorCode);
                }
            }
            
            //order
            var CheckOrder = await _mediator.Send(new EditOrderCommand(
                request.OrderID,
                request.OrderNumber,
                request.Items,
                request.Status,
                request.Comment,
                shippingAddressId
            ));

            if (!CheckOrder.Data)
            {
                return RequestResult<bool>.Failure(CheckOrder.ErrorCode);
            }

            return RequestResult<bool>.Success(true);
        }
    }
}
