using AutoMapper;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public class CompanyVerifyStatusDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public VerifyStatus VerifyStatus { get; set; }
        public string GovernorateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string ClassificationId { get; set; }
        public string ClassificationName { get; set; }
    }

    public class CompanyVerifyStatusDTOProfile : Profile
    {
        public CompanyVerifyStatusDTOProfile()
        {
            CreateMap<Company, CompanyVerifyStatusDTO>()
                .ForMember(dest => dest.GovernorateName, opt => opt.MapFrom(src => src.Governorate.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.VerifyStatus, opt => opt.MapFrom(src => src.User.VerifyStatus))
                .ForMember(dest => dest.ClassificationName, opt => opt.MapFrom(src => src.Classification.Name));
        }
    }
}
