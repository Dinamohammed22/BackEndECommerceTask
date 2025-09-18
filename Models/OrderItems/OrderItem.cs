using KOG.ECommerce.Models.Orders;
using KOG.ECommerce.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.OrderItems
{
    [Table("OrderItem", Schema = "OrderItems")]

    public class OrderItem:BaseModel
    {
        public double ItemPrice { get; set; }
        public double Price { get; set; }
        public double NetPrice { get; set; } = 0;
        public string? DiscountId { get; set; }
        public double DiscountAmount { get; set; }
        public int Quantity {  get; set; }
        public double ItemLiter { get; set; }
        public double Liter { get; set; }
        public int ItemPoint { get; set; }
        public int Point { get; set; }

        [ForeignKey("Product")]
        public string ProductId {  get; set; }
        public Product Product { get; set; }
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
