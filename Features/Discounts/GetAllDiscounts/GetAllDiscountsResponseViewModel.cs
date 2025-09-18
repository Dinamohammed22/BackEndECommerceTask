using AutoMapper;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.GetAllDiscounts
{
    public record GetAllDiscountsResponseViewModel(
        string ID,
        string Name,
        string DiscountCategory,
        string DiscountType,    
        int Quantity,
        double ReceiptAmount,
        double Amount,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive);
    public class GetAllDiscountsResponseProfile : Profile
    {
        public GetAllDiscountsResponseProfile()
        {
            CreateMap<GetAllDiscountsDTO, GetAllDiscountsResponseViewModel>();
        }
    }

}
