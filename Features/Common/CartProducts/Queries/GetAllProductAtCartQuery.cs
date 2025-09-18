using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.CartProducts;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.CartProducts.Queries
{
    public record GetAllProductAtCartQuery() : IRequestBase<IEnumerable<GetAllProductAtCartDTO>>;
    public class GetAllProductAtCartQueryHandler : RequestHandlerBase<CartProduct, GetAllProductAtCartQuery, IEnumerable<GetAllProductAtCartDTO>>
    {
        public GetAllProductAtCartQueryHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetAllProductAtCartDTO>>> Handle(GetAllProductAtCartQuery request, CancellationToken cancellationToken)
        {
           var CartProducts=_repository.Get(c=>c.CartId==_userState.UserID)
                .Include(cp => cp.Product).ThenInclude(p => p.Company)
                .Map<GetAllProductAtCartDTO>();
            return RequestResult<IEnumerable<GetAllProductAtCartDTO>>.Success(CartProducts);
        }
    }
}
