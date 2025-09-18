using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.FilterProductAdminPage
{
    public record SearchProductResponseViewModel(
                                      string ID,
                                      string ProductName,
                                      string CategoryName,
                                      string CategoryId,
                                      string SubcategoryName,
                                      double Price,
                                      int Quantity,
                                      string ImagePath,
                                      bool IsActive, 
                                      int NumberOfPoints,
                                      bool IsActivePoint,
                                      string CompanyId,
                                      string CompanyName,
                                      Grade Grade,
                                      double TotalPrice,
                                      double TotalWeight
                                   );

    public class SearchProductResponseProfile : Profile
    {
        public SearchProductResponseProfile()
        {
            CreateMap<SearchProductProfileDTO, SearchProductResponseViewModel>();
        }
    }

}
