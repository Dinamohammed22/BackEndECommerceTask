using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.CompanyGovernorates;
using KOG.ECommerce.Models.ShippingAddresses;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Governorates
{
    [Table("Governorate", Schema = "Governorates")]
    public class Governorate : BaseModel, ISelectableListItem
    {
        public string Name { get; set; }
        public string GovernorateCode {  get; set; }
        public bool IsActive { get; set; }
        public ICollection<City>? Cities { get; set; }
        public ICollection<Company>? Companies { get; set; }
        public ICollection<ShippingAddress>? ShippingAddresses { get; set; }
        public ICollection<CompanyGovernorate> CompanyGovernorates { get; set; }

    }
}
