using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Notifications
{
    [Table("NotificationMessage", Schema = "NotificationMessages")]
    public class NotificationMessage:BaseModel
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Token { get; set; }
    }
}
