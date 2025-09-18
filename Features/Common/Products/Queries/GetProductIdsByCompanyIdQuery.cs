using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductIdsByCompanyIdQuery(string CompanyId) : IRequestBase<IEnumerable<GetProductIdsByCompanyIdDTO>>;
    public class GetProductIdsByCompanyIdQueryHandler : RequestHandlerBase<Product, GetProductIdsByCompanyIdQuery, IEnumerable<GetProductIdsByCompanyIdDTO>>
    {
        public GetProductIdsByCompanyIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetProductIdsByCompanyIdDTO>>> Handle(GetProductIdsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var ProductIds =  _repository.Get(p => p.CompanyId == request.CompanyId).MapList<GetProductIdsByCompanyIdDTO>();
            return RequestResult<IEnumerable<GetProductIdsByCompanyIdDTO>>.Success(ProductIds);
        }
    }
}
