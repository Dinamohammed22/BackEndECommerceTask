using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;

namespace KOG.ECommerce.Features.Companies.CompanyReports
{
    public class CompanyReportsResponseViewModel
    {
        public string ID { get; set; }
        public string CompanyCode { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string GovernorateId { get; set; }
        public string GovernorateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public double? TotalPrice { get; set; } = 0;
        public double? TotalNetPrice { get; set; } = 0;
        public double? TotalLiter { get; set; } = 0;
    }

    public class CompanyReportsResponseProfile : Profile
    {
        public CompanyReportsResponseProfile()
        {
            CreateMap<CompaniesReportsDTO, CompanyReportsResponseViewModel>();
        }
    }
}
