using System.Xml.Linq;

namespace KOG.ECommerce.Features.Common.Orders.DTOs
{
    public class DiscountCalculationResultDTO
    {
        public double TotalPrice { get; set; }
        public double TotalNetPrice { get; set; }
        public List<double> ItemsNetPrice { get; set; } = new List<double>();
        public List<double> ItemsDiscountAmount { get; set; } = new List<double>();
    }

}
