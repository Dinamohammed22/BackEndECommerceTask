using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.ShippingAddresses.EditShippingAddress.Commands
{
    public record EditShippingAddressCommand(string ID,string GovernorateId, string CityId, string Street, string Landmark, double Latitude,
        double Longitude, string BuildingData) : IRequestBase<bool>;
    public class EditShippingAddressCommandHandler : RequestHandlerBase<ShippingAddress, EditShippingAddressCommand, bool>
    {
        public EditShippingAddressCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var checkID =await _repository.AnyAsync(s=>s.ID== request.ID);
            if (!checkID)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            var ClientId= _repository.GetByID(request.ID).ClientId;
            var check = await _mediator.Send(new CheckDoublicateAddressQuery(
                ShippingAddressId:request.ID,
                ClientId: ClientId,
                GovernorateId: request.GovernorateId,
                CityId: request.CityId,
                Street: request.Street,
                BuildingData: request.BuildingData));
            if (!check.IsSuccess)
                return RequestResult<bool>.Failure(check.ErrorCode);
            var shippingAddress = new ShippingAddress();
            shippingAddress.ID = request.ID;
            shippingAddress.GovernorateId=request.GovernorateId;
            shippingAddress.CityId=request.CityId;
            shippingAddress.Street=request.Street;
            shippingAddress.Longitude=request.Longitude;
            shippingAddress.Latitude=request.Latitude;
            shippingAddress.Landmark=request.Landmark;
            shippingAddress.BuildingData=request.BuildingData;
            shippingAddress.Status = ShippingAddressStatus.Pending;
            _repository.SaveIncluded(shippingAddress,nameof(shippingAddress.GovernorateId),nameof(shippingAddress.CityId),
                nameof(shippingAddress.Street), nameof(shippingAddress.Longitude), nameof(shippingAddress.Latitude),
                nameof(shippingAddress.Landmark),nameof(shippingAddress.BuildingData),nameof(shippingAddress.Status));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
