using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Emails
{
    [Table("Email", Schema = "Emails")]
    public class Email:BaseModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> EmailAdresses { get; set; }
    }
}
