using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;

namespace KOG.ECommerce.Features.ShippingAddresses.GetIDOfDefualtShippingAddress
{
    public record GetIDOfDefualtShippingAddressRequestViewModel(string ClientId);
    public class GetIDOfDefualtShippingAddressRequestValidator:AbstractValidator<GetIDOfDefualtShippingAddressRequestViewModel>
    {
        public GetIDOfDefualtShippingAddressRequestValidator() { }
    }
    public class GetIDOfDefualtShippingAddressRequestProfile:Profile
    {
        public GetIDOfDefualtShippingAddressRequestProfile()
        {
            CreateMap<GetIDOfDefualtShippingAddressRequestViewModel, GetIDOfDefualtShippingAddressQuery>();
        }
    }
}
