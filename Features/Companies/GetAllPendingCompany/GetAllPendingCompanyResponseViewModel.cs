using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Companies.GetAllPendingCompany
{
    public record GetAllPendingCompanyResponseViewModel(
        string ID,
        string Name,
        string Mobile,
        VerifyStatus VerifyStatus,
        string GovernorateName,
        string CityName
    );
    public class GetAllPendingCompanyResponseProfile : Profile
    {
        public GetAllPendingCompanyResponseProfile()
        {
            CreateMap<CompanyVerifyStatusDTO, GetAllPendingCompanyResponseViewModel>();
        }
    }
}
