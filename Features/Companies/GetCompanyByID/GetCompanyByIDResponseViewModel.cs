using AutoMapper;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.CompanyGovernorates.DTOs;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Models.CompanyGovernorates;

namespace KOG.ECommerce.Features.Companies;

public class GetCompanyByIDResponseViewModel
{
    public string ID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string GovernorateId { get; set; }
    public string GovernorateName { get; set; }
    public string CityId { get; set; }
    public string CityName { get; set; }
    public string TaxCardID { get; set; }
    public string TaxRegistryNumber { get; set; }
    public string NID { get; set; }
    public string ManagerName { get; set; }
    public string ManagerMobile { get; set; }
    public string ClassificationId { get; set; }
    public string ClassificationName { get; set; }
    public int MinimumQuantity { get; set; }
    public bool IsActive { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CreditLimit { get; set; }
    public List<CompanyGovernorateDTO> GovernorateIds { get; set; }
    public IEnumerable<MediaDTO> CompanyFiles { get; set; }
    public IEnumerable<MediaDTO> CompanyImage { get; set; }
}
public class GetCompanyByIDResponseProfile : Profile
{
    public GetCompanyByIDResponseProfile()
    {
        CreateMap<CompanyProfileDTO, GetCompanyByIDResponseViewModel>();
    }
}
