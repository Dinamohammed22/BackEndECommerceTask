namespace KOG.ECommerce.Common.DTOs
{
    public class SMSResponse
    {
        public long new_msg_id {  get; set; }
        public double transaction_price { get; set; }
        public double net_balance {  get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
