using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Carts
{
    [Table("Cart", Schema = "Carts")]
    public class Cart : BaseModel
    {
        [Key, ForeignKey("Client")]
        public override string ID { get; set; }
        public Client  Client { get; set; }

        public bool Status { get; set; }
        public ICollection<CartProduct>  CartProducts { get; set; }
    }
}
