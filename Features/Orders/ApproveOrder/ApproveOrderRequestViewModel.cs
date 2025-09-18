using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Orders.ApproveOrder.Commands;
using KOG.ECommerce.Features.Orders.ApproveOrder.Orchistrator;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Orders.ApproveOrder
{
    public record ApproveOrderRequestViewModel(string ID);
    public class ChangeOrderStatusRequestValidator:AbstractValidator<ApproveOrderRequestViewModel>
    {
        public ChangeOrderStatusRequestValidator()
        {

        }
    }
    public class ChangeOrderStatusRequestProfile:Profile
    {
        public ChangeOrderStatusRequestProfile()
        {
            CreateMap<ApproveOrderRequestViewModel, ApproveOrderOrchistrator>();
        }
    }
}
