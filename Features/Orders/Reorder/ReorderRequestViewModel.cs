using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Orders.Reorder.Orchestrators;

namespace KOG.ECommerce.Features.Orders.Reorder
{
    public record ReorderRequestViewModel(string OrderId);
    public class ReorderRequestValidator : AbstractValidator<ReorderRequestViewModel>
    {
        public ReorderRequestValidator() { }
    }
    public class ReorderRequestProfile : Profile
    {
        public ReorderRequestProfile() {
            CreateMap<ReorderRequestViewModel, ReorderOrchestrator>();
            CreateMap<ReorderOrchestrator, GetAllOrderItemByIDQuery>();
            //CreateMap<OrderItemDTO, GetAllProductAtCartDTO>();
        }
    }
}
