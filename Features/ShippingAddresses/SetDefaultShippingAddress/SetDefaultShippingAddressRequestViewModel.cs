using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ShippingAddresses.SetDefaultShippingAddress.Commands;

namespace KOG.ECommerce.Features.ShippingAddresses.SetDefaultShippingAddress
{
    public record SetDefaultShippingAddressRequestViewModel(string ID, string ClientId);
    public class SetDefaultShippingAddressRequestValidator : AbstractValidator<SetDefaultShippingAddressRequestViewModel>
    {
        public SetDefaultShippingAddressRequestValidator() { }
    }
    public class SetDefaultShippingAddressRequestProfile:Profile
    {
        public SetDefaultShippingAddressRequestProfile()
        {
            CreateMap<SetDefaultShippingAddressRequestViewModel, SetDefaultShippingAddressCommand>();
        }
    }
}
