using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.CartProducts.DTOs;
using KOG.ECommerce.Features.Common.CartProducts.Queries;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductLiterQuery(string ProductId) : IRequestBase<double>;
    public class GetProductsTotalLiterQueryHandler : RequestHandlerBase<Product, GetProductLiterQuery, double>
    {
        public GetProductsTotalLiterQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<double>> Handle(GetProductLiterQuery request, CancellationToken cancellationToken)
        {
            double ProductLiter = await _repository.Get(p=>p.ID == request.ProductId).Select(p=>p.Liter).FirstOrDefaultAsync(cancellationToken);

            return RequestResult<double>.Success(ProductLiter);
        }
    }
}
