using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Products.GetProductsByCategoryId
{
    public record GetProductsByCategoryIdResponse(
          string Name,
          string Description,
          string Model,
          double Price,
          double Tax,
          Brand Brand,
          int MinimumQuantity,
          int Quantity,
          int MaximumQuantity,
          double Length,
          double Width,
          double Height,
          double Weight,
          DateTime AvailableDate
    );

    public class GetProductsByCategoryIdResponseProfile : Profile
    {
        public GetProductsByCategoryIdResponseProfile()
        {
            CreateMap<GetProductsByCategoryIdDTO, GetProductsByCategoryIdResponse>();
        }
    }
}
