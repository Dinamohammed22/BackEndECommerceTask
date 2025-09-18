using AutoMapper;
using KOG.ECommerce.Models.Companies;

namespace KOG.ECommerce.Features.Common.Companies.DTOs
{
    public record GetCompaniesNamesDTO(string? ID, string? Name, int? NumberOfProducts);
    public class GetCompaniesNamesDTOProfile:Profile
    {
        public GetCompaniesNamesDTOProfile()
        {
            CreateMap<Company, GetCompaniesNamesDTO>();
        }
    }
}
