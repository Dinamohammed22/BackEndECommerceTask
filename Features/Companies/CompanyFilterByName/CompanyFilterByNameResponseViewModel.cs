using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;

namespace KOG.ECommerce.Features.Companies.CompanyFilterByName
{
    public class CompanyFilterByNameResponseViewModel
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
        public double TotalPrice { get; set; }
        public double TotalNetPrice { get; set; }
        public double TotalLiter { get; set; }
    }

    public class CompanyFilterByNameResponseProfile : Profile
    {
        public CompanyFilterByNameResponseProfile()
        {
            CreateMap<GetAllCompaniesDTO, CompanyFilterByNameResponseViewModel>();
        }
    }
}
