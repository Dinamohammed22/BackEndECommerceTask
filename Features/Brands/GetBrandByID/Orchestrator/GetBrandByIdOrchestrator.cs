using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.GetBrandByID.Orchestrator
{
    public record GetBrandByIdOrchestrator(string ID) : IRequestBase<BrandProfileDTO>;
    public class GetBrandByIdOrchestratorHandler : RequestHandlerBase<Brand, GetBrandByIdOrchestrator, BrandProfileDTO>
    {
        public GetBrandByIdOrchestratorHandler(RequestHandlerBaseParameters<Brand> parameters) : base(parameters) { }

        public async override Task<RequestResult<BrandProfileDTO>> Handle(GetBrandByIdOrchestrator request, CancellationToken cancellationToken)
        {
            var Brand = await _mediator.Send(request.MapOne<GetBrandByIDQuery>());
            if (Brand == null || Brand.Data == null)
            {
                return RequestResult<BrandProfileDTO>.Failure(Brand?.ErrorCode ?? ErrorCode.NotFound);
            }

            var pathsResult = await _mediator.Send(request.MapOne<CheckBrandHasMediaQuery>());
            var mediaList = pathsResult.IsSuccess && pathsResult.Data != null
                ? pathsResult.Data.ToList()
                : new List<MediaDTO>();

            var BrandWithMedia = Brand.Data.MapOne<BrandProfileDTO>() with
            {
                Media = mediaList
            };

            return RequestResult<BrandProfileDTO>.Success(BrandWithMedia);
        }

    }
}
