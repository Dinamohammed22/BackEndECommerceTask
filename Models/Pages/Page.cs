using KOG.ECommerce.Models.Modules;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Pages
{
    [Table("Page", Schema = "Pages")]
    public class Page : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Module> modules { get; set; }
    }
}
