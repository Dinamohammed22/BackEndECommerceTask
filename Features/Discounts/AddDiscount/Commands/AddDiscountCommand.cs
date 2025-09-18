using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Discounts.AddDiscount.Commands
{
    public record AddDiscountCommand(string Name, DiscountCategory DiscountCategory, DiscountType DiscountType, int? Quantity, double? ReceiptAmount, double Amount, DateTime StartDate, DateTime EndDate,bool IsActive) : IRequestBase<bool>;
    public class AddDiscountCommandHandler : RequestHandlerBase<Discount, AddDiscountCommand, bool>
    {
        public AddDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            var currentDiscounts = await _mediator.Send(request.MapOne<CheckDiscountQuery>());
            if (currentDiscounts.Data.IsNullOrEmpty()) {
                Discount discount = new Discount
                {
                    Name = request.Name,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    DiscountType = request.DiscountType,
                    ReceiptAmount = request.ReceiptAmount ?? 0.0,
                    Quantity = request.Quantity ?? 0,

                    Amount = request.Amount,
                    DiscountCategory = request.DiscountCategory,
                    IsActive = request.IsActive
                };
                _repository.Add(discount);
                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            if (request.DiscountCategory == DiscountCategory.General)
            {
                return RequestResult<bool>.Failure(ErrorCode.CannotAddDiscount);  
            }
            foreach(var item in currentDiscounts.Data)
            {
                if (item.DiscountCategory == DiscountCategory.General) {
                    return RequestResult<bool>.Failure(ErrorCode.CannotAddDiscount);
                }
              
            }

            return RequestResult<bool>.Success(true);

        }
    }
}
