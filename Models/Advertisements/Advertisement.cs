using KOG.ECommerce.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Advertisements
{
    [Table("Advertisement", Schema = "Advertisements")]
    public class Advertisement : BaseModel
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public ImageType ImageTypes { get; set; }
        public string? Hyperlink { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
