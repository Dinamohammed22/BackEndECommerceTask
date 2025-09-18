using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.DiscountProducts;

namespace KOG.ECommerce.Features.DiscountProducts.AddDiscountToProduct.Commands
{
    public record AddDiscountToProductCommand(double Percentage, DateTime StartDate, DateTime EndDate, bool IsActive, List<string> ProductIds, string DiscountId) : IRequestBase<bool>;

    public class AddDiscountToProductCommandHandler : RequestHandlerBase<DiscountProduct, AddDiscountToProductCommand, bool>
    {
        public AddDiscountToProductCommandHandler(RequestHandlerBaseParameters<DiscountProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddDiscountToProductCommand request, CancellationToken cancellationToken)
        {
            
            foreach (var product in request.ProductIds)
            {
                var discountProduct = new DiscountProduct
                {
                    ProductId = product,
                    DiscountId = request.DiscountId,
                    IsActive = request.IsActive,
                };
                _repository.Add(discountProduct);
            }

            _repository.SaveChanges();


            return RequestResult<bool>.Success(true);
        }
    }

}
