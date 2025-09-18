using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Features.Common.OrderItems.DTOs;
using KOG.ECommerce.Features.Common.Orders.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Orders;
using MailKit.Search;
using MediatR;

namespace KOG.ECommerce.Features.Common.Orders.Queries
{
    public record CalculateDiscountQuery(
       List<EditOrderDTO> Items,
       GetAllDiscountsDTO? CurrentDiscount
    ) : IRequestBase<DiscountCalculationResultDTO>;

    public class CalculateDiscountQueryHandler : RequestHandlerBase<Order,CalculateDiscountQuery, DiscountCalculationResultDTO>
    {
        public CalculateDiscountQueryHandler(RequestHandlerBaseParameters<Order> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<DiscountCalculationResultDTO>> Handle(CalculateDiscountQuery request, CancellationToken cancellationToken)
        {
            double totalPrice = request.Items.Sum(item => item.Quantity * item.ItemPrice);
            double totalNetPrice = 0;
            List<double> itemsNetPrice = new List<double>();
            List<double> itemsDiscountAmount = new List<double>();

            if (request.CurrentDiscount != null)
            {
                if (request.CurrentDiscount.DiscountCategory == DiscountCategory.Receipt)
                {
                    int totalQuantity = request.Items.Sum(item => item.Quantity);
                    var receiptDiscountResult = await _mediator.Send(new ApplyReceiptDiscountQuery(
                        totalPrice,
                        request.CurrentDiscount.Amount,
                        request.CurrentDiscount.ReceiptAmount,
                        request.CurrentDiscount.DiscountType
                    ));

                    totalNetPrice = receiptDiscountResult.Data;
                    itemsNetPrice.AddRange(request.Items.Select(item => item.ItemPrice * item.Quantity));
                    itemsDiscountAmount = Enumerable.Repeat(0.0, request.Items.Count).ToList();
                }
                else if (request.CurrentDiscount.DiscountCategory == DiscountCategory.General)
                {
                    foreach (var item in request.Items)
                    {
                        var price = item.ItemPrice * item.Quantity;
                        var generalDiscountResult = await _mediator.Send(new ItemGeneralDiscountQuery(
                            price,
                            item.Quantity,
                            request.CurrentDiscount.Amount,
                            request.CurrentDiscount.DiscountType
                        ));

                        itemsNetPrice.Add(price - generalDiscountResult.Data);
                        itemsDiscountAmount.Add(generalDiscountResult.Data);
                    }

                    totalNetPrice = itemsNetPrice.Sum();
                }
            }
            else
            {
                totalNetPrice = totalPrice;
                itemsNetPrice.AddRange(request.Items.Select(item => item.ItemPrice * item.Quantity));
                itemsDiscountAmount = Enumerable.Repeat(0.0, request.Items.Count).ToList();
            }

            var DiscountData = new DiscountCalculationResultDTO
            {
                TotalPrice = totalPrice,
                TotalNetPrice = totalNetPrice,
                ItemsNetPrice = itemsNetPrice,
                ItemsDiscountAmount = itemsDiscountAmount
            };

            return RequestResult<DiscountCalculationResultDTO>.Success(DiscountData);

        }


    }
}
