using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress.Commands;

namespace KOG.ECommerce.Features.ShippingAddresses.ApproveShippingAddress
{
    public record ApproveShippingAddressRequestViewModel(string ShippingAddressId);
    public class ApproveShippingAddressRequestValidator : AbstractValidator<ApproveShippingAddressRequestViewModel>
    {
        public ApproveShippingAddressRequestValidator()
        {
        }
    }
    public class ApproveShippingAddressRequestProfile : Profile
    {
        public ApproveShippingAddressRequestProfile()
        {
            CreateMap<ApproveShippingAddressRequestViewModel,ApproveShippingAddressCommand>();
        }
    }
}
