using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands
{
    public record CreateShippingAddressCommand(string GovernorateId, string CityId, string Street, string Landmark, double Latitude,
        double Longitude, string ClientId, bool? IsDefualt,string BuildingData, ShippingAddressStatus Status) :IRequestBase<bool>;
    public class CreateShippingAddressCommandHandler : RequestHandlerBase<ShippingAddress, CreateShippingAddressCommand, bool>
    {
        public CreateShippingAddressCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateShippingAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddressCount = await _mediator.Send(new CheckExistingAddressCountQuery(request.ClientId));

            if (!existingAddressCount.IsSuccess)
            {
                return RequestResult<bool>.Failure(existingAddressCount.ErrorCode);
            }
            var check = await _mediator.Send(new CheckDoublicateAddressQuery(
                ShippingAddressId: null,
                ClientId: request.ClientId,
                GovernorateId: request.GovernorateId,
                CityId: request.CityId,
                Street: request.Street,
                BuildingData: request.BuildingData));
            if (!check.IsSuccess)
                return RequestResult<bool>.Failure(check.ErrorCode);

            ShippingAddress shippingAddress = new ShippingAddress
            {
                GovernorateId = request.GovernorateId,
                CityId = request.CityId,
                Street = request.Street,
                Landmark = request.Landmark,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                 BuildingData=request.BuildingData,
                ClientId = request.ClientId,
                IsDefualt = request.IsDefualt??false,
                Status = request.Status,
            };
            _repository.Add(shippingAddress);
            _repository.SaveChanges();

            var result = RequestResult<bool>.Success(true);

            return await Task.FromResult(result);
        }
    }


}
