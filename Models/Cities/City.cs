using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Governorates;
using KOG.ECommerce.Models.ShippingAddresses;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Cities
{
    [Table("City", Schema = "Cities")]
    public class City:BaseModel, ISelectableListItem
    {
        public string Name {  get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Governorate")]
        public string GovernorateId {  get; set; }
        public Governorate Governorate { get; set; }
        public ICollection<Company>?  Companies { get; set;}
        public ICollection<ShippingAddress>? ShippingAddresses { get; set; }
    }
}
