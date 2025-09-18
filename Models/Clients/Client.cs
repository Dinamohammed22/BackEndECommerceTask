using KOG.ECommerce.Models.Carts;
using KOG.ECommerce.Models.ClientGroups;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KOG.ECommerce.Models.Wishlists;
using KOG.ECommerce.Models.ShippingAddresses;
using KOG.ECommerce.Models.Orders;

namespace KOG.ECommerce.Models.Clients
{
    [Table("Client", Schema = "Clients")]

    public class Client : BaseModel
    {
        public string? NationalNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public Religion Religion {  get; set; }
        //public bool IsActive { get; set; }
        [Key, ForeignKey("User")]
        public override string ID { get; set; }
        public User User { get; set; }
        [ForeignKey("ClientGroup")]
        public string? ClientGroupId {  get; set; }
        public ClientGroup? ClientGroup { get; set; }
        public string? Email { get; set; }
        public double Wallet { get; set; }
        public int NumberOfPoints {  get; set; }
        public Cart? Cart { get; set; }
        public Wishlist? WishList { get; set; }
        public ClientActivity? ClientActivity {  get; set; }
        public ICollection<ShippingAddress> ShippingAddresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
