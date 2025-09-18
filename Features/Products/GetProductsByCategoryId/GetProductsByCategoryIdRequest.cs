using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.GetProductsByCategoryId
{
    public record GetProductsByCategoryIdRequest(string CategoryId, int pageIndex = 1, int pageSize = 100);
    public class GetProductsByCategoryIdRequestValidator : AbstractValidator<GetProductsByCategoryIdRequest>
    {
        public GetProductsByCategoryIdRequestValidator()
        {
        }
    }

    public class GetProductsByCategoryIdRequestProfile : Profile
    {
        public GetProductsByCategoryIdRequestProfile()
        {
            CreateMap<GetProductsByCategoryIdRequest, GetProductsByCategoryIdQuery>();
        }
    }
}
