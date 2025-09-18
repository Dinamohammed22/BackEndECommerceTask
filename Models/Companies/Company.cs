using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Classifications;
using KOG.ECommerce.Models.CompanyGovernorates;
using KOG.ECommerce.Models.Coupons;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Companies
{
    [Table("Company", Schema = "Companies")]
    public class Company : BaseModel, ISelectableListItem
    {
        public string CompanyCode {  get; set; }
        [Key, ForeignKey("User")]
        public override string ID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        [ForeignKey("Governorate")]
        public string GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        [ForeignKey("City")]
        public string CityId { get; set; }
        public City City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Activity { get; set; }
        public string? TaxCardID { get; set; }
        public string? TaxRegistryNumber { get; set; }
        public string? CreditLimit { get; set; }
        public string? NID { get; set; }
        public string? ManagerName { get; set; }
        public string? ManagerMobile { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Classification")]
        public string? ClassificationId { get; set; }
        public Classification Classification { get; set; }
        public ICollection<Coupon>? Coupons { get; set; }
        public ICollection<Product> products { get; set; }
        public ICollection<CompanyGovernorate> CompanyGovernorates { get; set; }
        public string? Email { get; set; }
        public int MinimumQuantity { get; set; } = 1;
    }
}

