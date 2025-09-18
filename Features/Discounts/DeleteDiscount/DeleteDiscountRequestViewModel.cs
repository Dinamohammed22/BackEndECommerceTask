using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Discounts.DeleteDiscount.Commands;

namespace KOG.ECommerce.Features.Discounts.DeleteDiscount
{
    public record DeleteDiscountRequestViewModel(string ID);
    public class DeleteDiscountRequestValidator:AbstractValidator<DeleteDiscountRequestViewModel>
    {
        public DeleteDiscountRequestValidator() { }

    }
    public class DeleteDiscountRequestProfile:Profile
    {
        public DeleteDiscountRequestProfile()
        {
            CreateMap<DeleteDiscountRequestViewModel, DeleteDiscountCommand>();
        }
    }


}
