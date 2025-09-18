using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.ShippingAddresses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Orders
{
    [Table("Order", Schema = "Orders")]

    public class Order:BaseModel
    {
        [MinLength(10)]
        [MaxLength(50)] 
        public string OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public double TotalNetPrice { get; set; }
        public string? DiscountId { get; set; }
        public double DiscountAmount { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalLiter { get; set; }
        public int TotalNumberOfPoints { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? InProcessDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Comment {  get; set; }
        [ForeignKey("ShippingAddress")]
        public string? ShippingAddressId {  get; set; }
        public ShippingAddress? ShippingAddress { get; set; }
        [ForeignKey("Client")]
        public string ClientId {  get; set; }
        public Client Client { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
