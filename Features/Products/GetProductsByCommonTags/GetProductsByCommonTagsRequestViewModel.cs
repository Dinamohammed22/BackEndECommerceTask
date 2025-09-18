using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Products.GetProductsByCategoryId;

namespace KOG.ECommerce.Features.Products.GetProductsByCommonTags
{
    public record GetProductsByCommonTagsRequestViewModel(string ProductID , int NumOfProducts = 3);
    public class GetProductsByCommonTagsRequestValidator : AbstractValidator<GetProductsByCommonTagsRequestViewModel>
    {
        public GetProductsByCommonTagsRequestValidator()
        {
        }
    }

    public class GetProductsByCommonTagsRequestProfile : Profile
    {
        public GetProductsByCommonTagsRequestProfile()
        {
            CreateMap<GetProductsByCommonTagsRequestViewModel, GetProductsByCommonTagsQuery>();
        }
    }
}
