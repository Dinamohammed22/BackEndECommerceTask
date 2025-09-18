using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record GetProductsByCommonTagsQuery(string ProductID, int NumOfProducts = 3) :IRequestBase<IEnumerable<ProductViewDTO>>;
    public class GetProductsByCommonTagsQueryHandler : RequestHandlerBase<Product, GetProductsByCommonTagsQuery, IEnumerable<ProductViewDTO>>
    {
        public GetProductsByCommonTagsQueryHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<ProductViewDTO>>> Handle(GetProductsByCommonTagsQuery request, CancellationToken cancellationToken)
        {
            var govResult = await _mediator.Send(new GetDefaultShippingGovernorateIdQuery(_userState.UserID));

            if (!govResult.IsSuccess)
                return RequestResult<IEnumerable<ProductViewDTO>>
                       .Failure(govResult.ErrorCode);

            var governorateId = govResult.Data;

            var product = await _repository.FirstOrDefaultAsync(p => p.ID == request.ProductID);
            if (product == null)
            {
                return RequestResult<IEnumerable<ProductViewDTO>>.Failure();
            }

            var productTags = product.Tags;

            var CommonProducts = _repository.Get(p => p.ID != request.ProductID && p.Company.IsActive &&
                     p.Company.CompanyGovernorates
                          .Any(cg => cg.GovernorateId == governorateId)).Include(p => p.Company).ThenInclude(p => p.CompanyGovernorates)
                .AsEnumerable()
                .Where(p => p.Tags.Any(tag => productTags.Contains(tag)))
                .Take(request.NumOfProducts)
                .MapList<ProductViewDTO>();
            if (CommonProducts.IsNullOrEmpty())
                return RequestResult<IEnumerable<ProductViewDTO>>.Success(CommonProducts);

            if (!CommonProducts.Any() || CommonProducts == null || productTags == null || !productTags.Any())
            {
                CommonProducts = _repository.Get(p => p.ID != request.ProductID)
                    .AsEnumerable()
                    .Where(p => p.CategoryId == product.CategoryId)
                    .Take(request.NumOfProducts)
                    .MapList<ProductViewDTO>();
            }

            var productViewDtos = new List<ProductViewDTO>();

            foreach (var p in CommonProducts)
            {
                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(p.ID, SourceType.Product));
                var productWithMedia = p.MapOne<ProductViewDTO>() with
                {
                    Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
                };
                productViewDtos.Add(productWithMedia);
            }

            return RequestResult<IEnumerable<ProductViewDTO>>.Success(productViewDtos);
        }
    }
}
