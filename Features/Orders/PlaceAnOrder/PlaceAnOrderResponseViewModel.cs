using AutoMapper;
using KOG.ECommerce.Features.Orders.Reorder;

namespace KOG.ECommerce.Features.Orders.PlacedAnOrder
{
    public record PlaceAnOrderResponseViewModel(string OrderNumber);
    public class PlaceAnOrderResponseProfile : Profile
    {
        public PlaceAnOrderResponseProfile()
        {
            CreateMap<string,PlaceAnOrderResponseViewModel>().ConstructUsing(OrderNumber => new PlaceAnOrderResponseViewModel(OrderNumber));
        }
    }
}
