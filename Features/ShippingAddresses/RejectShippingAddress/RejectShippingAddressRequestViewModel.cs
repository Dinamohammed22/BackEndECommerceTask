using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ShippingAddresses.RejectShippingAddress.Commands;

namespace KOG.ECommerce.Features.ShippingAddresses.RejectShippingAddress
{
    public record RejectShippingAddressRequestViewModel(string ShippingAddressId);
    public class RejectShippingAddressRequestValidator : AbstractValidator<RejectShippingAddressRequestViewModel>
    {
        public RejectShippingAddressRequestValidator()
        {
        }
    }
    public class RejectShippingAddressRequestProfile : Profile
    {
        public RejectShippingAddressRequestProfile()
        {
            CreateMap<RejectShippingAddressRequestViewModel, RejectShippingAddressCommand>();
        }
    }
}
