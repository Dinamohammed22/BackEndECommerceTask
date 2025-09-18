using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KOG.ECommerce.Models.Users
{
    [Table("User", Schema = "Users")]

    public class User:BaseModel
    {
        public string Name { get; set; }
        public string Password {  get; set; }
        public string Mobile { get; set; }
        public Client Client { get; set; }
        public Company Company { get; set; }
        public bool IsActive { get; set; }
        public Role RoleId { get; set; }
        public VerifyStatus VerifyStatus { get; set; }
        public string? RejectReason {  get; set; }
        public string? JobTitle { get; set; }
        public string? OTP {  get; set; }
        public string? OTPtoken {  get; set; }
        public string? FirebaseToken { get; set; }
        public DateTime? OTPExpiration { get; set; }
    }
}
