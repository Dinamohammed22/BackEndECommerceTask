using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public class GetAllCompaniesDTO
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
    public class GetAllCompaniesDTOProfile : Profile
    {
        public GetAllCompaniesDTOProfile()
        {
            CreateMap<Company, GetAllCompaniesDTO>()
                .ForMember(dest => dest.ClassificationName, opt => opt.MapFrom(src => src.Classification.Name ?? string.Empty))
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name ?? string.Empty))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name ?? string.Empty));
            CreateMap<ExportCompanyToExcelQuery, CompanyFilterByNameQuery>();
        }
    }
}
