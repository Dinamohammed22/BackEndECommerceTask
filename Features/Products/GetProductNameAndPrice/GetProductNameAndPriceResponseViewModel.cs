using AutoMapper;
using KOG.ECommerce.Features.Common.Products.DTOs;

namespace KOG.ECommerce.Features.Products.GetProductNameAndPrice
{
    public class GetProductNameAndPriceResponseViewModel
    {
        public string ID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }
    public class GetProductNameAndPriceResponseProfile:Profile
    {
        public GetProductNameAndPriceResponseProfile()
        {
            CreateMap<GetProductNameAndPriceDTO, GetProductNameAndPriceResponseViewModel>();
        }
    }
}
