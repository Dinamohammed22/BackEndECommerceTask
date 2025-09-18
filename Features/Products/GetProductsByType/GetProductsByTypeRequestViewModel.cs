using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.GetProductsByType
{
    public record GetProductsByTypeRequestViewModel(GetProductType GetProductType, int NumberOfProducts = 3);
    public class GetProductsByTypeRequestValidator : AbstractValidator<GetProductsByTypeRequestViewModel>
    {
        public GetProductsByTypeRequestValidator() { }
    }
    public class GetProductsByTypeRequestProfile : Profile
    {
        public GetProductsByTypeRequestProfile() {
            CreateMap<GetProductsByTypeRequestViewModel, GetProductsByTypeQuery>();
        }
    }
}
