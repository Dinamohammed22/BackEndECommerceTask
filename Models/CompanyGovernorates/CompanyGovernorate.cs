using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Governorates;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.CompanyGovernorates
{
    [Table("CompanyGovernorate", Schema = "CompanyGovernorates")]
    public class CompanyGovernorate : BaseModel
    {
        [ForeignKey("Governorate")]
        public string GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        [ForeignKey("Company")]
        public string CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
