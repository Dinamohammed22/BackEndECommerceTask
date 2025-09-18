using AutoMapper;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.GetDiscountByID
{
    public record GetDiscountByIDResponseViewModel(
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
    public class GetDiscountByIDResponseProfile:Profile
    {
        public GetDiscountByIDResponseProfile()
        {
            CreateMap<GetAllDiscountsDTO, GetDiscountByIDResponseViewModel>();
        }
    }
}
