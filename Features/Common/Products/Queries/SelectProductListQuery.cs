using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record SelectProductListQuery() : IRequestBase<IEnumerable<ProductSelectListDTO>>;
    public class SelectProductListQueryHandler : RequestHandlerBase<Product, SelectProductListQuery, IEnumerable<ProductSelectListDTO>>
    {
        public SelectProductListQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductSelectListDTO>>> Handle(SelectProductListQuery request, CancellationToken cancellationToken)
        {
            var selectListItems = _repository.Get()
                .Include(p => p.Company)
                .MapList<ProductSelectListDTO>();
            return RequestResult<IEnumerable<ProductSelectListDTO>>.Success(selectListItems);
        }
    }

}
