using AutoMapper;
using KOG.ECommerce.Models.OrderItems;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public class GetPriceAndWeightForProductDTO
    {
        public double TotalPrice { get; set; }
        public double TotalLiter { get; set; }
    }
    public class GetPriceAndWeightForProductDTOProfile:Profile
    {
        public GetPriceAndWeightForProductDTOProfile()
        {
            CreateMap<OrderItem, GetPriceAndWeightForProductDTO>();
        }
    }
}
