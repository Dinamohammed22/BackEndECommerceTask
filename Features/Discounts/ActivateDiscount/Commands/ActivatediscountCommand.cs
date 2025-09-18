using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct.Commands;
using KOG.ECommerce.Models.Discounts;

namespace KOG.ECommerce.Features.Discounts.ActivateDiscount.Commands
{
    public record ActivatediscountCommand(string ID) : IRequestBase<bool>;
    public class ActivatediscountCommandHandler : RequestHandlerBase<Discount, ActivatediscountCommand, bool>
    {
        public ActivatediscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivatediscountCommand request, CancellationToken cancellationToken)
        {

            var check = _repository.Any(d => d.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Discount discount = new Discount { ID = request.ID };
            discount.IsActive = true;
            _repository.SaveIncluded(discount, nameof(discount.IsActive));
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
    }

}
