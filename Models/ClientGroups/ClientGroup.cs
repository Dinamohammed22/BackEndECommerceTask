using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Clients;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.ClientGroups
{
    [Table("ClientGroup", Schema = "ClientGroup")]
    public class ClientGroup:BaseModel, ISelectableListItem
    {
        public string Name {  get; set; }
        public bool TaxExempted { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
