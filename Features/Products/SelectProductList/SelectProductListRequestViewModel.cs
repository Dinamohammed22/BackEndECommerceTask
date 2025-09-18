using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.SelectProductList
{
    public record SelectProductListRequestViewModel();
    public class SelectProductListRequestValidator:AbstractValidator<SelectProductListRequestViewModel>
    {
        public SelectProductListRequestValidator() { }
    }
    public class SelectProductListRequestProfile : Profile
    {
        public SelectProductListRequestProfile()
        {
            CreateMap<SelectProductListRequestViewModel, SelectProductListQuery>();
        }
    }
}
