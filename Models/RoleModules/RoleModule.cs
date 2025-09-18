using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Modules;
using System.ComponentModel.DataAnnotations.Schema;


namespace KOG.ECommerce.Models.RoleModules
{
    [Table("RoleModule", Schema = "RoleModules")]
    public class RoleModule: BaseModel
    {
        //[ForeignKey("Role")]
        //public string RoleId { get; set; }
        public Role RoleId { get; set; }

        [ForeignKey("Module")]
        public string ModuleId {  get; set; }
        public Module Module { get; set; }
    }
}
