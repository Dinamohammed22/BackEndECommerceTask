using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.DTOs;
﻿using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.GetProductById.Orchestrator
{
    public record GetProductByIDWithMediaOrchestrator(string ID) : IRequestBase<ProductWithMediaDTO>;
    public class GetProductByIDWithMediaOrchestratorHandler : RequestHandlerBase<Product, GetProductByIDWithMediaOrchestrator, ProductWithMediaDTO>
    {
        public GetProductByIDWithMediaOrchestratorHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters) { }

        public async override Task<RequestResult<ProductWithMediaDTO>> Handle(GetProductByIDWithMediaOrchestrator request, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(request.MapOne<GetProductByIDWithMediaQuery>());
            if (product == null || product.Data == null)
            {
                return RequestResult<ProductWithMediaDTO>.Failure(product?.ErrorCode ?? ErrorCode.NotFound);
            }

            var pathsResult = await _mediator.Send(request.MapOne<CheckProductHasMediaQuery>());
            var mediaList = pathsResult.IsSuccess && pathsResult.Data != null
                ? pathsResult.Data.ToList()
                : new List<MediaDTO>();

            var productWithMedia = product.Data.MapOne<ProductWithMediaDTO>();
            productWithMedia.Media = mediaList;


            return RequestResult<ProductWithMediaDTO>.Success(productWithMedia);
        }

    }
}
