using KOG.ECommerce.Models.Carts;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.CartProducts
{
    public class CartProduct:BaseModel
    {
        [ForeignKey("Product")]
        public string ProductId {  get; set; }
        [ForeignKey("Cart")]
        public string CartId { get; set; }  
        public Product Product { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }

    }
}
