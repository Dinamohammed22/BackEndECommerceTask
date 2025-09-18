using AutoMapper;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Discounts.DTOs
{
    public record GetAllDiscountsDTO(
        string ID,
        string Name,
        DiscountCategory DiscountCategory, 
        DiscountType DiscountType,         
        int Quantity,
        double ReceiptAmount,
        double Amount,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive);
    public class GetAllDiscountsDTOProfile : Profile
    {
        public GetAllDiscountsDTOProfile()
        {
            CreateMap<Discount, GetAllDiscountsDTO>()
                .ForMember(dest => dest.DiscountCategory,
                    opt => opt.MapFrom(src => Enum.GetName(typeof(DiscountCategory), src.DiscountCategory) ?? "Unknown"))
                .ForMember(dest => dest.DiscountType,
                    opt => opt.MapFrom(src => Enum.GetName(typeof(DiscountType), src.DiscountType) ?? "Unknown"));
        }
    }

}
