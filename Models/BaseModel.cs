using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KOG.ECommerce.Models
{
    public class BaseModel
    {
        [Key]
        public virtual string ID { get; set; }= Guid.NewGuid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; } = string.Empty;

        public bool Deleted { get; set; } = false;
    }
}
