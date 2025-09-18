using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetProductsByBrandId
{
    public record GetProductsByBrandIdRequestViewModel(string BrandId);
    public class GetProductsByBrandIdRequestValidator : AbstractValidator<GetProductsByBrandIdRequestViewModel>
    {
        public GetProductsByBrandIdRequestValidator()
        {
        }
    }
    public class GetProductsByBrandIdRequestProfile : Profile
    {
        public GetProductsByBrandIdRequestProfile()
        {
            CreateMap<GetProductsByBrandIdRequestViewModel, GetProductsByBrandIdQuery>();
        }
    }
}
