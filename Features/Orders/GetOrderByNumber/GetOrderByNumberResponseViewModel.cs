using AutoMapper;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrderByNumber
{
    public record GetOrderByNumberResponseViewModel
        (string OrderNumber, double TotalPrice, double TotalNetPrice,
        OrderStatus Status, DateTime CreatedDate, string OrderId, string CustomerName,
        string Mobile, string Email, List<OrderItemsDTO> OrderItems,
         double TotalLiter);
    public class GetOrderByNumberResponseProfile:Profile
    {
        public GetOrderByNumberResponseProfile()
        {
            CreateMap<GetOrderByNumberDTO, GetOrderByNumberResponseViewModel>();
        }
    }

}
