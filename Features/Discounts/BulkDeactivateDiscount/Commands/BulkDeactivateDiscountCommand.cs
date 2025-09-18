using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Discounts.BulkDeactivateDiscount.Commands
{
    public record BulkDeactivateDiscountCommand(List<string> Ids) : IRequestBase<bool>;

    public class BulkDeactivateDiscountCommandHandler : RequestHandlerBase<Discount, BulkDeactivateDiscountCommand, bool>
    {
        public BulkDeactivateDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateDiscountCommand request, CancellationToken cancellationToken)
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
                                              calls.SetProperty(d => d.IsActive, false)
                                          );

            if (affectedRows == 0)
                return RequestResult<bool>.Failure(ErrorCode.CannotEdit);

            return RequestResult<bool>.Success(true);
        }
    }
}
