using AutoMapper;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.DTOs
{
    public class GetProductIdsByCompanyIdDTO
    {
        public string ID { get; set; }
    }
    public class GetProductIdsByCompanyIdDTOProfile:Profile
    {
        public GetProductIdsByCompanyIdDTOProfile()
        {
            CreateMap<Product, GetProductIdsByCompanyIdDTO> ();
        }
    }
}
