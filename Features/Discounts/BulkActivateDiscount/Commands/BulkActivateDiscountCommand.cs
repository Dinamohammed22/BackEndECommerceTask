using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Discounts.BulkActivateDiscount.Commands
{
    public record BulkActivateDiscountCommand(List<string> Ids):IRequestBase<bool>;
    public class BulkActivateDiscountCommandHandler : RequestHandlerBase<Discount, BulkActivateDiscountCommand, bool>
    {
        public BulkActivateDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingIds = _repository.Get()
                                         .Where(d => request.Ids.Contains(d.ID))
                                         .Select(d => d.ID)
                                         .ToList();

            if (existingIds.Count != request.Ids.Count)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            var affectedRows = _repository.Get()
                                          .Where(d => request.Ids.Contains(d.ID))
                                          .ExecuteUpdate(calls =>
                                              calls.SetProperty(d => d.IsActive, true)
                                          );

            if (affectedRows == 0)
                return RequestResult<bool>.Failure(ErrorCode.CannotEdit);

            return RequestResult<bool>.Success(true);
        }
    }
}
