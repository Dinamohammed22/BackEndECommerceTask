using AutoMapper;

namespace KOG.ECommerce.Features.Orders.PlaceOrderByAdmin
{
    public class PlaceOrderByAdminResponseViewModel(string OrderNumber);
    public class PlaceOrderByAdminResponseProfile : Profile
    {
        public PlaceOrderByAdminResponseProfile()
        {
            CreateMap<string, PlaceOrderByAdminResponseViewModel>().ConstructUsing(OrderNumber => new PlaceOrderByAdminResponseViewModel(OrderNumber));
        }
    }
}
