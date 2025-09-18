using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.CartProducts;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.CartProducts.Queries
{
    public record GetAllProductAtCartWithPriceQuery() : IRequestBase<GetAllProductAtCartWithTotalPriceDTO>;

    public class GetAllProductAtCartWithPriceQueryHandler : RequestHandlerBase<CartProduct, GetAllProductAtCartWithPriceQuery, GetAllProductAtCartWithTotalPriceDTO>
    {
        public GetAllProductAtCartWithPriceQueryHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetAllProductAtCartWithTotalPriceDTO>> Handle(GetAllProductAtCartWithPriceQuery request, CancellationToken cancellationToken)
        {
            var cartProducts = await _repository.Get(c => c.CartId == _userState.UserID)
                                                 .Include(cp => cp.Product).ThenInclude(p => p.Company)
                                                 .ToListAsync(cancellationToken);

            if (cartProducts == null || !cartProducts.Any())
            {
                return RequestResult<GetAllProductAtCartWithTotalPriceDTO>.Failure(ErrorCode.NotFound);
            }

            var productDTOs = new List<GetAllProductAtCartWithPriceDTO>();
            double totalPrice = 0;

            foreach (var cartProduct in cartProducts)
            {
                // Fetch media path using mediator
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(cartProduct.ProductId, SourceType.Product), cancellationToken);

                string mediaPath = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;

                var productDTO = cartProduct.MapOne<GetAllProductAtCartWithPriceDTO>();
                productDTO.Path = mediaPath;

                productDTOs.Add(productDTO);
                totalPrice += cartProduct.Quantity * cartProduct.Product.Price;
            }

            var result = new GetAllProductAtCartWithTotalPriceDTO(productDTOs, totalPrice);
            return RequestResult<GetAllProductAtCartWithTotalPriceDTO>.Success(result);
        }
    }
}
