using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Categories.DTOs;
using KOG.ECommerce.Features.Common.Categories.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetListProduct
{
    public record GetProductRequestViewModel();

    public class GetProductRequestValidator : AbstractValidator<GetProductRequestViewModel>
    {
        public GetProductRequestValidator()
        {

        }
    }

    public class GetProductRequestProfile : Profile
    {
        public GetProductRequestProfile()
        {
            CreateMap<GetProductRequestViewModel, GetProductQuery>();
        }
    }

}
