using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.Wishlists;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.WishlistProducts
{
    [Table("WishlistProduct", Schema = "WishlistProducts")]
    public class WishlistProduct : BaseModel
    {
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        [ForeignKey("Wishlist")]
        public string WishlistId { get; set; }
        public Product Product { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
