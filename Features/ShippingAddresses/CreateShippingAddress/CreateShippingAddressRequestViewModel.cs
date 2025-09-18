using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress
{
    public record CreateShippingAddressRequestViewModel(string GovernorateId, string CityId, string Street, string Landmark, double Latitude,
        double Longitude, string ClientId, bool? IsDefualt,string BuildingData ,ShippingAddressStatus Status);
    public class CreateShippingAddressRequestValidator:AbstractValidator<CreateShippingAddressRequestViewModel>
    {
        public CreateShippingAddressRequestValidator()
        {
            RuleFor(x => x.GovernorateId)
                .NotEmpty().WithMessage("Governorate ID is required.");


            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("City ID is required.");


            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");


            RuleFor(x => x.Landmark)
                .MaximumLength(200).WithMessage("Landmark cannot exceed 200 characters.");


            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");


            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
        }
    }
    public class CreateShippingAddressEndPointRequestProfile : Profile
    {
        public CreateShippingAddressEndPointRequestProfile()
        {
            CreateMap<CreateShippingAddressRequestViewModel, CreateShippingAddressCommand>();
        }
    }
}
