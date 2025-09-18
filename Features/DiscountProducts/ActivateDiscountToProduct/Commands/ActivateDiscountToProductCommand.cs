using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.DiscountProducts.DeactivateDiscountToProduct.Commands;
using KOG.ECommerce.Models.DiscountProducts;
using KOG.ECommerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.DiscountProducts.ActivateDiscountToProduct.Commands
{
    public record ActivateDiscountToProductCommand(string ProductId, string DiscountId) : IRequestBase<bool>;
    public class ActivateDiscountToProductCommandHandler : RequestHandlerBase<DiscountProduct, ActivateDiscountToProductCommand, bool>
    {
        public ActivateDiscountToProductCommandHandler(RequestHandlerBaseParameters<DiscountProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateDiscountToProductCommand request, CancellationToken cancellationToken)
        {
            var discountProduct =await _repository.FirstOrDefaultAsync(dp => dp.ProductId == request.ProductId && dp.DiscountId == request.DiscountId);

            if (discountProduct == null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            discountProduct.IsActive = true;
            _repository.SaveIncluded(discountProduct, nameof(discountProduct.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
