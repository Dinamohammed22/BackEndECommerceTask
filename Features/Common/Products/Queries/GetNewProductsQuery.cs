using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetNewProductsQuery(int NumberOfProducts=3):IRequestBase<IEnumerable<ProductViewDTO>>;
    public class GetNewProductsQueryHandler : RequestHandlerBase<Product, GetNewProductsQuery, IEnumerable<ProductViewDTO>>
    {
        public GetNewProductsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetNewProductsQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<IEnumerable<ProductViewDTO>>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var newProducts = await _repository
                .Get(c => c.IsActive && c.AvailableDate <= DateTime.Now &&
                     c.Company.IsActive &&
                     c.Company.CompanyGovernorates
                          .Any(cg => cg.GovernorateId == governorateId)).Include(p=>p.Company)
                           .ThenInclude(p => p.CompanyGovernorates)
                .OrderByDescending(p => p.AvailableDate)
                .Take(request.NumberOfProducts)
                .ToListAsync();

            var productViewDtos = new List<ProductViewDTO>();

            foreach (var product in newProducts)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(product.ID, SourceType.Product));
                var productWithMedia = product.MapOne<ProductViewDTO>() with
                {
                    Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
                };
                productViewDtos.Add(productWithMedia);
            }

            return RequestResult<IEnumerable<ProductViewDTO>>.Success(productViewDtos);
        }

    }
}