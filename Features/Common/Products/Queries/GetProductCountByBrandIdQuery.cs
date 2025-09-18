using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductCountByBrandIdQuery(string BrandId,string? CategoryId) : IRequestBase<GetProductCountByBrandIdDTO>;
    public class GetProductCountByBrandIdQueryHandler : RequestHandlerBase<Product, GetProductCountByBrandIdQuery, GetProductCountByBrandIdDTO>
    {
        public GetProductCountByBrandIdQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<GetProductCountByBrandIdDTO>> Handle(GetProductCountByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<GetProductCountByBrandIdDTO>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var productCount = await _repository
                .Get(p => p.BrandId == request.BrandId &&
                          (string.IsNullOrEmpty(request.CategoryId) || p.CategoryId == request.CategoryId) &&
                     p.IsActive &&
                     p.Company.IsActive &&
                     p.Company.CompanyGovernorates
                          .Any(cg => cg.GovernorateId == governorateId))
                .Include(p => p.Company)
                .ThenInclude(c => c.CompanyGovernorates)
                .CountAsync();

            var result = new GetProductCountByBrandIdDTO(productCount);

            return RequestResult<GetProductCountByBrandIdDTO>.Success(result);
        }
    }
}
