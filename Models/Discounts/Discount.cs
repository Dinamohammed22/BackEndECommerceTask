using KOG.ECommerce.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Discounts
{
    [Table("Discount", Schema = "Discounts")]
    public class Discount : BaseModel
    {
        public string Name { get; set; }
        public double Amount {  get; set; }
        public DiscountCategory DiscountCategory { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Quantity { get; set; } = 0;
        public double ReceiptAmount { get; set; } = 0.0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
