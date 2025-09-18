using AutoMapper;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;

namespace KOG.ECommerce.Features.CartProducts.GetAllProductAtCart
{
    public class GetAllProductAtCartResponseViewModel
    {
        public String ProductId { get; set; }
        public String CompanyId { get; set; }
        public String CompanyName { get; set; }
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
    }
    public class GetAllProductAtCartResponseProfile : Profile
    {
        public GetAllProductAtCartResponseProfile() {
            CreateMap<GetAllProductAtCartDTO,GetAllProductAtCartResponseViewModel>();
        }
    }
}
