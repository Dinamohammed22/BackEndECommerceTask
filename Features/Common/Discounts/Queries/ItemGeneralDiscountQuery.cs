using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    
    public record ItemGeneralDiscountQuery(double Price ,int Quantity, double Amount, DiscountType DiscountType ) : IRequestBase<double>;
    public class ItemGeneralDiscountQueryHandler : RequestHandlerBase<Discount, ItemGeneralDiscountQuery, double>
    {
        public ItemGeneralDiscountQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<double>> Handle(ItemGeneralDiscountQuery request, CancellationToken cancellationToken)
        {
            double DiscountAmount = 0;

            if (request.DiscountType == DiscountType.Percentage)
            {
                DiscountAmount = request.Price * (request.Amount / 100);
            }
            else if (request.DiscountType == DiscountType.Money)
            {
                DiscountAmount = request.Amount * request.Quantity;
            }
            //double NetPrice = request.Price - DiscountAmount;

            return RequestResult<double>.Success(DiscountAmount);
        }
    }
}
