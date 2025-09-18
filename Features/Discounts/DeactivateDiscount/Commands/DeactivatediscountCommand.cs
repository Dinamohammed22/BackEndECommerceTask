using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.DiscountProducts;
using KOG.ECommerce.Models.Discounts;

namespace KOG.ECommerce.Features.Discounts.DeactivateDiscountToProduct.Commands
{
    public record DeactivatediscountCommand(string ID) : IRequestBase<bool>;
    public class DeactivatediscountCommandHandler : RequestHandlerBase<Discount, DeactivatediscountCommand, bool>
    {
        public DeactivatediscountCommandHandler(RequestHandlerBaseParameters<Discount> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactivatediscountCommand request, CancellationToken cancellationToken)
        {
            var check = _repository.Any(d => d.ID == request.ID);

            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            Discount discount = new Discount { ID = request.ID};
            discount.IsActive = false;
            _repository.SaveIncluded(discount, nameof(discount.IsActive));
            _repository.SaveChanges();
           
            return RequestResult<bool>.Success(true);
        }
    }
}
