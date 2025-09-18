using AutoMapper;
using KOG.ECommerce.Features.Cities.Queries.GetLisCity;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Categories;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Features.Products.GetListProduct
{
    public record GetProductResponseViewModel(string ProductName,
                                              string Description,
                                              string CategoryName,
                                              List<string> Tags,
                                              string Model,
                                              double Price,
                                              double Tax,
                                              string BrandName,
                                              int MinimumQuantity,
                                              int MaximumQuantity,
                                              double Length,
                                              double Width,
                                              double Height,
                                              double Weight,
                                              DateTime AvailableDate,
                                              int NumberOfPoints
                                              );
    public class GetProductResponseProfile : Profile
    {
        public GetProductResponseProfile()
        {
            CreateMap<ProductProfileDTO, GetProductResponseViewModel>();

        }
    }


}
