using KOG.ECommerce.Common.Interfaces;
using KOG.ECommerce.Models.Companies;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Classifications
{
    [Table("Classification", Schema = "Classifications")]

    public class Classification:BaseModel, ISelectableListItem
    {
        public string Name {  get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
