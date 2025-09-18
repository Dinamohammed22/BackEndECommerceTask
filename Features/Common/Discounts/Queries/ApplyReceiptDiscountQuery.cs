using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KOG.ECommerce.Features.Common.Discounts.Queries
{
    public record ApplyReceiptDiscountQuery(double TotalPrice, double Amount, double ReceiptAmount , DiscountType DiscountType) : IRequestBase<double>;
    public class ApplyReceiptDiscountQueryHandler : RequestHandlerBase<Discount, ApplyReceiptDiscountQuery, double>
    {
        public ApplyReceiptDiscountQueryHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<double>> Handle(ApplyReceiptDiscountQuery request, CancellationToken cancellationToken)
        {
            if (request.TotalPrice >= request.ReceiptAmount)
            {
                if (request.DiscountType == DiscountType.Percentage)
                {
                    double discountValue = request.TotalPrice * (request.Amount / 100);
                    return RequestResult<double>.Success(request.TotalPrice - discountValue);
                }
                else if (request.DiscountType == DiscountType.Money)
                {
                    return RequestResult<double>.Success(request.TotalPrice - request.Amount);
                }
            }

            return RequestResult<double>.Success(request.TotalPrice);
        }
    }
}
