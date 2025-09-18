using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.CreateClient.Orchestrators;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.Clients.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Features.Orders.PlacedAnOrder.Orchestrators;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByAdmin.Orchistrator
{
    public record PlaceOrderByAdminOrchisterator(
        //client data
        string? NationalNumber,
        string? Name,
        string? Password,
        string? ConfirmPassword,
        string Mobile,
        string? Email,
        string? Phone,
        string? ClientGroupId,
        Religion? Religion,
        List<string>? Paths, 
        ClientActivity? ClientActivity,
        //shipping adress data
        string? GovernorateId,
        string? CityId,
        string? Street,
        string? Landmark, 
        string? ShippingAddressId,
        //order data
        string? Comment,
        string? BuildingData,
        //default value data
        IEnumerable<GetAllProductAtCartDTO> cartProductsResult,
        bool IsDefualt = false,
        bool NotifyCustomer = true,
        double Latitude = 0.0,
        double Longitude = 0.0) : IRequestBase<string>;

    public class PlaceOrderByAdminOrchisteratorHandler : RequestHandlerBase<Order, PlaceOrderByAdminOrchisterator, string>
    {
        public PlaceOrderByAdminOrchisteratorHandler(RequestHandlerBaseParameters<Order> requestParameters) :  base(requestParameters)
        {
        }
        public override async Task<RequestResult<string>> Handle(PlaceOrderByAdminOrchisterator request, CancellationToken cancellationToken)
        {
            string ClientId = null;
            string ShippingAddressId = null;

            var ClientData = await _mediator.Send(new GetClientByMobileQuery(request.Mobile));

            if (ClientData.IsSuccess)
            {
                ClientId = ClientData.Data.UserId;
                if (string.IsNullOrEmpty(request.ShippingAddressId) && string.IsNullOrEmpty(request.GovernorateId))
                {
                    ShippingAddressId = ClientData.Data.ShippingAddresses.ID;
                }
                else
                {
                    ShippingAddressId = request.ShippingAddressId;
                }
            }
            else
            {
                //create client
                var userId = await _mediator.Send(new CreateClientOrchestrator(
                   Name: request.Name,
                   Password: request.Password,
                   Mobile: request.Mobile,
                   ConfirmPassword: request.ConfirmPassword,
                   Email: request.Email,
                   ClientGroupId: request.ClientGroupId,
                   NationalNumber: request.NationalNumber,
                   CityId: request.CityId,
                   Street: request.Street,
                   Landmark: request.Landmark,
                   Latitude: request.Latitude,
                   Longitude: request.Longitude,
                   GovernorateId: request.GovernorateId,
                   Phone:request.Phone,
                   Paths:request.Paths,
                   ClientActivity:request.ClientActivity??0,
                   BuildingData:request.BuildingData,
                   Religion: request.Religion ?? Religion.Islam
                ));

                if(!userId.IsSuccess)
                {
                    return RequestResult<string>.Failure(userId.ErrorCode);
                }

                ClientId = userId.Data;
                var NewClientShippingAddress = await _mediator.Send(new GetShippingAddressIdByClientIDQuery(ClientId));
                ShippingAddressId = NewClientShippingAddress.Data;
            }

            var OrderNumber = await _mediator.Send(new PlaceOrderOrchestrator(
                OrderStatus.InProcess,
                ClientId,
                request.Comment,
                ShippingAddressId,
                request.GovernorateId,
                request.CityId,
                request.Street,
                request.Landmark,
                request.Latitude,
                request.Longitude,
                request.IsDefualt,
                request.cartProductsResult,
                request.BuildingData,
                ShippingAddressStatus.Approved
            ));

            if (!OrderNumber.IsSuccess)
            {
                return RequestResult<string>.Failure(OrderNumber.ErrorCode, OrderNumber.Message);
            }

            return RequestResult<string>.Success(OrderNumber.Data);
        }
    }

}
