using KOG.ECommerce.Models.Clients;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KOG.ECommerce.Models.WishlistProducts;

namespace KOG.ECommerce.Models.Wishlists
{
    [Table("Wishlist", Schema = "Wishlists")]
    public class Wishlist : BaseModel
    {
        [Key]
        [ForeignKey("Client")]
        public override string ID { get; set; }
        public bool IsActive { get; set; }
        public Client Client { get; set; }

        public ICollection<WishlistProduct>? WishlistProducts { get; set; }

    }
}
