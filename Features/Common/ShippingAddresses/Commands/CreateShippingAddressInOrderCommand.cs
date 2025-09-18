using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.ShippingAddresses;

namespace KOG.ECommerce.Features.Common.ShippingAddresses.Commands
{
    public record CreateShippingAddressInOrderCommand(
        string GovernorateId,
        string CityId,
        string Street,
        string Landmark,
        double Latitude,
        double Longitude,
        string ClientId, 
        string BuildingData
       ) : IRequestBase<string>;

    public class CreateShippingAddressInOrderCommandHandler : RequestHandlerBase<ShippingAddress, CreateShippingAddressInOrderCommand, string>
    {
        public CreateShippingAddressInOrderCommandHandler(RequestHandlerBaseParameters<ShippingAddress> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateShippingAddressInOrderCommand request, CancellationToken cancellationToken)
        {
            var existingAddressCount = _repository.
                                            Get(sa => sa.ClientId == request.ClientId)
                                            .Count();

            if (existingAddressCount >= 3)
            {
                return RequestResult<string>.Failure(ErrorCode.AlreadyHasThreeShippingAddresses);
            }
            var check = await _mediator.Send(new CheckDoublicateAddressQuery(
                ShippingAddressId:null,
                ClientId: request.ClientId,
                GovernorateId: request.GovernorateId,
                CityId: request.CityId,
                Street: request.Street,
                BuildingData: request.BuildingData));
            if (!check.IsSuccess)
                return RequestResult<string>.Failure(check.ErrorCode);
            ShippingAddress shippingAddress = new ShippingAddress
            {
                GovernorateId = request.GovernorateId,
                CityId = request.CityId,
                Street = request.Street,
                Landmark = request.Landmark,
                Latitude = request.Latitude ,
                Longitude = request.Longitude ,
                ClientId = request.ClientId,
                IsDefualt = false ,
                Status = ShippingAddressStatus.Pending,
                BuildingData = request.BuildingData,
            };

            _repository.Add(shippingAddress);
            _repository.SaveChanges();

            var result = RequestResult<string>.Success(shippingAddress.ID);
            return await Task.FromResult(result);
        }
    }
}
