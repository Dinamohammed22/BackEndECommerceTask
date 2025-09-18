using AutoMapper;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.GetOrderHistory
{
    public record GetOrderHistoryResponseViewModel(string ID,
    string OrderNumber,
    string Name,
    string Mobile,
    OrderStatus Status,
    double TotalNetPrice,
    double TotalPrice,
    int TotalQuantity,
    DateTime Date);
    public class GetOrderHistoryResponseProfile : Profile
    {
        public GetOrderHistoryResponseProfile()
        {
            CreateMap<GetOrderHistoryDTO, GetOrderHistoryResponseViewModel>();
        }
    }
}
