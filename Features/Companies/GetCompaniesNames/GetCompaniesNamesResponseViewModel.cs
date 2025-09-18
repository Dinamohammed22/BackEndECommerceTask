using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;

namespace KOG.ECommerce.Features.Companies.GetCompaniesNames
{
    public record GetCompaniesNamesResponseViewModel(string? ID, string? Name, int? NumberOfProducts);
    public class GetCompaniesNamesResponseProfile:Profile
    {
        public GetCompaniesNamesResponseProfile()
        {
            CreateMap<GetCompaniesNamesDTO, GetCompaniesNamesResponseViewModel>();
        }
    }
}
