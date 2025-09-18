using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;


namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddressesForClient
{
    public record GetShippingAddressesForClientRequestViewModel(string ClientId);
    public class GetShippingAddressesForClientRequestValidator : AbstractValidator<GetShippingAddressesForClientRequestViewModel>
    {
        public GetShippingAddressesForClientRequestValidator() { }
    }
    public class GetShippingAddressesForClientRequestProfile : Profile
    {
        public GetShippingAddressesForClientRequestProfile()
        {
            CreateMap<GetShippingAddressesForClientRequestViewModel, GetShippingAddressesForClientQuery>();
        }
    }


}
