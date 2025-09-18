using AutoMapper;

namespace KOG.ECommerce.Features.ShippingAddresses.GetIDOfDefualtShippingAddress
{
    public record GetIDOfDefualtShippingAddressResponseViewModel(string ID);
    public class GetIDOfDefualtShippingAddressResponseProfile:Profile
    {
        public GetIDOfDefualtShippingAddressResponseProfile()
        {
            CreateMap<string, GetIDOfDefualtShippingAddressResponseViewModel>()
            .ConstructUsing(ID => new GetIDOfDefualtShippingAddressResponseViewModel(ID));
        }
    }
}
