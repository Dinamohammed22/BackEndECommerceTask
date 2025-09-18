using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.SelectProductList
{
    public record SelectProductListResponseViewModel(string ID, string Name, string CompanyId, string CompanyName, int MinimumQuantity, int MaximumQuantity);
    public class SelectProductListResponseProfile:Profile
    {
        public SelectProductListResponseProfile()
        {
            CreateMap<ProductSelectListDTO, SelectProductListResponseViewModel>();
        }
    }
}
