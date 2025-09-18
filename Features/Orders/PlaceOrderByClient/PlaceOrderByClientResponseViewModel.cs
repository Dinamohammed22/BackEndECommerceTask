using AutoMapper;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByClient
{
    public record PlaceOrderByClientResponseViewModel(string OrderNumber);
    public class PlaceOrderByClientResponseProfile : Profile
    {
        public PlaceOrderByClientResponseProfile() { 
            CreateMap<string,PlaceOrderByClientResponseViewModel>().ConstructUsing(OrderNumber => new PlaceOrderByClientResponseViewModel(OrderNumber));
        }
    }
}
