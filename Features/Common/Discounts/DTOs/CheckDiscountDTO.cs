using AutoMapper;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Discounts.DTOs
{
    public record CheckDiscountDTO(string ID,DiscountCategory DiscountCategory,DiscountType DiscountType, double Amount, double ReceiptAmount);
    public class CheckDiscountProfileDTO:Profile
    {
        public CheckDiscountProfileDTO()
        {
            CreateMap<Discount, CheckDiscountDTO>();
        }
    }
}
