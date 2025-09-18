using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Models.Discounts;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Discounts.EditDiscount.Commands
{
    public record EditDiscountCommand(string ID, string Name, DiscountCategory DiscountCategory, DiscountType DiscountType,
        double? ReceiptAmount, int? Quantity, double Amount, DateTime StartDate, DateTime EndDate, bool IsActive) : IRequestBase<bool>;
    public class EditDiscountCommandHandler : RequestHandlerBase<Discount, EditDiscountCommand, bool>
    {
        public EditDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(EditDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount = await _repository.GetByIDAsync(request.ID);
            if (existingDiscount == null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var currentDiscounts = await _mediator.Send(new CheckDiscountQuery(request.StartDate, request.EndDate));
            var Discounts = currentDiscounts.Data.Where(D => D.ID != request.ID).ToList();
            if (Discounts != null && Discounts.Any())
            {
               
                    return RequestResult<bool>.Failure(ErrorCode.CannotEdit);
            }
            

            existingDiscount.Name = request.Name;
            existingDiscount.DiscountCategory = request.DiscountCategory;
            existingDiscount.DiscountType = request.DiscountType;
            existingDiscount.ReceiptAmount = request.ReceiptAmount ?? existingDiscount.ReceiptAmount;
            existingDiscount.Quantity = request.Quantity ?? existingDiscount.Quantity;
            existingDiscount.Amount = request.Amount;
            existingDiscount.StartDate = request.StartDate;
            existingDiscount.EndDate = request.EndDate;
            existingDiscount.IsActive = request.IsActive;
            _repository.SaveIncluded(existingDiscount,
                nameof(existingDiscount.Name),
                nameof(existingDiscount.DiscountCategory),
                nameof(existingDiscount.DiscountType),
                nameof(existingDiscount.ReceiptAmount),
                nameof(existingDiscount.Quantity),
                nameof(existingDiscount.Amount),
                nameof(existingDiscount.StartDate),
                nameof(existingDiscount.EndDate),
                nameof(existingDiscount.IsActive));

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
        //public async override Task<RequestResult<bool>> Handle(EditDiscountCommand request, CancellationToken cancellationToken)
        //{
        //    var check = await _repository.AnyAsync(b => b.ID == request.ID);
        //    if (!check)
        //        return RequestResult<bool>.Failure(ErrorCode.NotFound);
        //    Discount discount = new Discount { ID = request.ID };
        //    discount.Name = request.Name;
        //    discount.DiscountCategory=request.DiscountCategory;
        //    discount.DiscountType=request.DiscountType;
        //    discount.ReceiptAmount=request.ReceiptAmount.Value;
        //    discount.Quantity=request.Quantity.Value;
        //    discount.Amount = request.Amount;
        //    discount.StartDate=request.StartDate;
        //    discount.EndDate=request.EndDate;
        //    _repository.SaveIncluded(discount, nameof(discount.Name), nameof(discount.DiscountCategory), nameof(discount.DiscountType), nameof(discount.ReceiptAmount),
        //         nameof(discount.Amount), nameof(discount.StartDate), nameof(discount.EndDate));
        //    _repository.SaveChanges();
        //    var result = RequestResult<bool>.Success(true);
        //    return await Task.FromResult(result);
        //}
    }
}
