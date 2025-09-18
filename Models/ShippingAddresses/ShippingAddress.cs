using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.ShippingAddresses
{
    [Table("ShippingAddress", Schema = "ShippingAddresses")]
    public class ShippingAddress:BaseModel
    {
        [ForeignKey("Governorate")]
        public string GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        [ForeignKey("City")]
        public string CityId { get; set; }
        public City City { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<Order> Orders { get; set; }
        public bool IsDefualt {  get; set; }
        public ShippingAddressStatus Status { get; set; }
        public string BuildingData { get; set; }
    }
}
