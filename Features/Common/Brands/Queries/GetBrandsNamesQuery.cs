using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record GetBrandsNamesQuery(List<string>? CategoryIds) : IRequestBase<IEnumerable<GetBrandsNamesDTO>>;

    public class GetBrandsNamesQueryHandler : RequestHandlerBase<Brand, GetBrandsNamesQuery, IEnumerable<GetBrandsNamesDTO>>
    {
        public GetBrandsNamesQueryHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetBrandsNamesDTO>>> Handle(GetBrandsNamesQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (govResult.Data == null && _userState.RoleID == Role.Client)
                return RequestResult<IEnumerable<GetBrandsNamesDTO>>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var brands = await _repository.Get(b => b.IsActive )
                .Include(b => b.Products)
                .ThenInclude(p => p.Company)
                .ThenInclude(c => c.CompanyGovernorates)
                .ToListAsync(cancellationToken);


            var result = new List<GetBrandsNamesDTO>();

            foreach (var brand in brands)
            {
                int totalProductCount = 0;

                // If CategoryIds are provided, filter by those categories
                if (request.CategoryIds != null && request.CategoryIds.Any())
                {
                    foreach (var categoryId in request.CategoryIds)
                    {
                        var productCount = await _mediator.Send(new GetProductCountByBrandIdQuery(brand.ID, categoryId));
                        if (productCount?.Data?.NumberOfProducts > 0)
                        {
                            totalProductCount += productCount.Data.NumberOfProducts;
                        }
                    }
                }
                // If no CategoryIds are provided, calculate totalProductCount for all categories
                else
                {
                    var productCount = await _mediator.Send(new GetProductCountByBrandIdQuery(brand.ID, null));
                    if (productCount?.Data?.NumberOfProducts > 0)
                    {
                        totalProductCount = productCount.Data.NumberOfProducts;
                    }
                }

                // Always add the brand to the result with its total product count
                result.Add(new GetBrandsNamesDTO
                (
                    brand.ID,
                    brand.Name,
                    totalProductCount
                ));
            }

            return RequestResult<IEnumerable<GetBrandsNamesDTO>>.Success(result);
        }
    }
}
