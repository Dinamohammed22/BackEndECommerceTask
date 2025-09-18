using KOG.ECommerce.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Medias
{
    [Table("Media", Schema = "Medias")]
    public class Media:BaseModel
    {
        public string SourceId {  get; set; }
        public SourceType SourceType {  get; set; }
        public string Path {  get; set; }
    }
}
