using KOG.ECommerce.Models.Companies;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Coupons
{
    [Table("Coupon", Schema = "Coupons")]
    public class Coupon:BaseModel
    {
        public string Name {  get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Amount { get; set; }
        public int MaxNumOfUser {  get; set; }
        [ForeignKey("Company")]
        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public bool IsActive { get; set; } 
    }
}
