using AutoMapper;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public record GetCompanyRequestByIdDTO
    {
        public string ID { get; set; }
        public string CompanyCode { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Activity { get; set; }
        public string TaxCardID { get; set; }
        public string TaxRegistryNumber { get; set; }
        public string CreditLimit { get; set; }
        public string NID { get; set; }
        public string ManagerName { get; set; }
        public string ManagerMobile { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public VerifyStatus VerifyStatus { get; set; }
        public string GovernorateName { get; set; }
        public string GovernorateId { get; set; }
        public string CityName { get; set; }
        public string CityId { get; set; }
        public string ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public int MinimumQuantity { get; set; }
        public IEnumerable<MediaDTO> CompanyFiles { get; set; }
        public IEnumerable<MediaDTO> CompanyImage { get; set; }
    }

    public class GetCompanyRequestByIdDTOProfile : Profile
    {
        public GetCompanyRequestByIdDTOProfile()
        {
            CreateMap<Company, GetCompanyRequestByIdDTO>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.VerifyStatus, opt => opt.MapFrom(src => src.User.VerifyStatus))
                .ForMember(dest => dest.ClassificationName, opt => opt.MapFrom(src => src.Classification.Name));
        }
    }
}
