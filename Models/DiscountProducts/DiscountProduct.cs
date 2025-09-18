using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.DiscountProducts
{
    [Table("DiscountProduct", Schema = "DiscountProducts")]
    public class DiscountProduct : BaseModel
    {
        [ForeignKey("Discount")]
        public string? DiscountId { get; set; }
        [ForeignKey("Product")]
        public string? ProductId { get; set; }

        public Discount? Discount { get; set; }
        public Product? Product { get; set; }
        public bool IsActive { get; set; }
    }
}
