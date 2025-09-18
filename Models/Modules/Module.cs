using KOG.ECommerce.Models.Pages;
using KOG.ECommerce.Models.RoleModules;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Modules
{
    [Table("Module", Schema = "Modules")]
    public class Module : BaseModel
    {
        public string Name { get; set; }
        [ForeignKey("page")]
        public string PageId { get; set; }
        public Page page { get; set; }
        public ICollection<RoleModule> RoleModules { get; set; }

    }
}
