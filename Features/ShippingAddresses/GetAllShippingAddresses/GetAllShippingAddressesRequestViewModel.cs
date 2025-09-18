using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;

namespace KOG.ECommerce.Features.ShippingAddresses.GetAllShippingAddresses
{
    public record GetAllShippingAddressesRequestViewModel(
    string? GovernorateId, string? CityId, string? Street, string? Landmark,
    double? Latitude, double? Longitude, string? ClientName, string? Mobile,
    bool? IsDefualt, int PageNumber = 1, int PageSize = 100);
    public class GetAllShippingAddressesRequestValidator : AbstractValidator<GetAllShippingAddressesRequestViewModel>
    {
        public GetAllShippingAddressesRequestValidator() { }
    }
    public class GetAllShippingAddressesRequestProfile : Profile
    {
        public GetAllShippingAddressesRequestProfile() {
            CreateMap<GetAllShippingAddressesRequestViewModel, GetAllShippingAddressesQuery>();
        }
    }
}
