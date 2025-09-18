using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Companies.GetAllApproveOrRejectCompany
{
    public record GetAllApproveOrRejectCompanyResponseViewModel(
        string ID,
        string Name, 
        string Mobile, 
        VerifyStatus VerifyStatus, 
        string GovernorateName, 
        string CityName
    );
    public class GetAllApproveOrRejectCompanyResponseProfile : Profile
    {
        public GetAllApproveOrRejectCompanyResponseProfile()
        {
            CreateMap<CompanyVerifyStatusDTO, GetAllApproveOrRejectCompanyResponseViewModel>();
        }
    }
}
