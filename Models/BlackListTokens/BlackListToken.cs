using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.BlackListTokens
{
    [Table("BlackListToken", Schema = "BlackListTokens")]
    public class BlackListToken:BaseModel
    {
        public string Token { get; set; }
    }
}
