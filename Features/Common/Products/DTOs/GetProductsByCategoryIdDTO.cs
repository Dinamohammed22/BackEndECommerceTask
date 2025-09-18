using AutoMapper;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
     public record GetProductsByCategoryIdDTO (
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
          double Liter,
          DateTime AvailableDate
     );

    public class GetProductsByCategoryIdProfile : Profile
    {
        public GetProductsByCategoryIdProfile()
        {
            CreateMap<Product, GetProductsByCategoryIdDTO>();
        }
    }
}
