using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Discounts.BulkDeleteDiscount.Commands
{
    public record BulkDeleteDiscountCommand(List<string> Ids):IRequestBase<bool>;
    public class BulkDeleteDiscountCommandHandler : RequestHandlerBase<Discount, BulkDeleteDiscountCommand, bool>
    {
        public BulkDeleteDiscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingIds = await _repository.Get()
                                               .Where(b => request.Ids.Contains(b.ID))
                                               .Select(b => b.ID)
                                               .ToListAsync(cancellationToken);

            if (existingIds.Count != request.Ids.Count)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            foreach (var id in request.Ids)
            {
                _repository.Delete(id);
            }

            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }
}
