using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Modules;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.ModuleFeatures
{
    [Table("ModuleFeature", Schema = "ModuleFeatures")]
    public class ModuleFeature:BaseModel
    {
        [ForeignKey("Module")]
        public string ModuleId { get; set; }

        public Module Module { get; set; }
        public Feature Features { get; set; }
    }
}
