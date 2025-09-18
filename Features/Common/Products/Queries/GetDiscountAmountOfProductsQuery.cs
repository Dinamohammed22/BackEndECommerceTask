using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetDiscountAmountOfProductsQuery(IEnumerable<GetPriceOfProductsDTO> GetPriceOfProducts,double Amount,DiscountType DiscountType):IRequestBase<IEnumerable<GetDiscountAmountOfProductsDTO>>;
    public class GetDiscountAmountOfProductsQueryHandler : RequestHandlerBase<Product, GetDiscountAmountOfProductsQuery, IEnumerable<GetDiscountAmountOfProductsDTO>>
    {
        public GetDiscountAmountOfProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetDiscountAmountOfProductsDTO>>> Handle(GetDiscountAmountOfProductsQuery request, CancellationToken cancellationToken)
        {
           
            if (request.GetPriceOfProducts == null || !request.GetPriceOfProducts.Any())
                return RequestResult<IEnumerable<GetDiscountAmountOfProductsDTO>>.Failure(ErrorCode.NotFound);

            if (request.Amount <= 0)
                return RequestResult<IEnumerable<GetDiscountAmountOfProductsDTO>>.Failure(ErrorCode.UnKnown);

            
            var discountedProducts = request.GetPriceOfProducts.Select(product =>
            {
                double discountAmount = 0;

                if (request.DiscountType == DiscountType.Money)
                {
                    
                    discountAmount = request.Amount*product.Quantity;
                }
                else if (request.DiscountType == DiscountType.Percentage)
                {
                    discountAmount = product.Price*product.Quantity * (request.Amount / 100);
                }

                return new GetDiscountAmountOfProductsDTO(product.ProductId, discountAmount);
            });

            return RequestResult<IEnumerable<GetDiscountAmountOfProductsDTO>>.Success(discountedProducts);
        }
    }


}
