using AutoMapper;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.OrderItems.DTOs
{
    public class CompanyTotalStaticsDTO
    {
        public double? TotalPrice { get; set; } = 0;
        public double? TotalNetPrice { get; set; } = 0;
        public double? TotalWeight { get; set; } = 0;
    }
    public class CompanyTotalStaticsDTOProfile : Profile
    {
        public CompanyTotalStaticsDTOProfile()
        {
        }
    }
}
