using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.CartProducts.Queries;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCart
{
    public record GetAllProductAtCartRequestViewModel();
    public class GetAllProductAtCartRequestValidator : AbstractValidator<GetAllProductAtCartRequestViewModel>
    {
        public GetAllProductAtCartRequestValidator() { }
    }
    public class GetAllProductAtCartRequestProfile : Profile
    {
        public GetAllProductAtCartRequestProfile() {

            CreateMap<GetAllProductAtCartRequestViewModel, GetAllProductAtCartQuery>();
        }
    }
}
