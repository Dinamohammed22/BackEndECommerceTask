using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Discounts;

namespace KOG.ECommerce.Features.Discounts.DeleteDiscount.Commands
{
    public record DeleteDiscountCommand(string ID):IRequestBase<bool>;
    public class DeleteDiscountCommandHandler : RequestHandlerBase<Discount, DeleteDiscountCommand, bool>
    {
        public DeleteDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }
}
