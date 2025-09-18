using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ShippingAddresses.DeleteShippingAddress.Commands;

namespace KOG.ECommerce.Features.ShippingAddresses.DeleteShippingAddress
{
    public record DeleteShippingAddressRequestViewModel(string ID);
    public class DeleteShippingAddressRequestValidator : AbstractValidator<DeleteShippingAddressRequestViewModel>
    {
        public DeleteShippingAddressRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty();
        }
    }
    public class DeleteShippingAddressEndPointRequestProfile : Profile
    {
        public DeleteShippingAddressEndPointRequestProfile()
        {
            CreateMap<DeleteShippingAddressRequestViewModel, DeleteShippingAddressCommand>();
        }
    }
}
