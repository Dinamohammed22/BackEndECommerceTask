using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Brands.Queries
{
    public record CheckBrandHasMediaQuery(string ID) : IRequestBase<IEnumerable<MediaDTO>>;

    public class CheckBrandHasMediaQueryHandler : RequestHandlerBase<Media, CheckBrandHasMediaQuery, IEnumerable<MediaDTO>>
    {
        public CheckBrandHasMediaQueryHandler(RequestHandlerBaseParameters<Media> parameters) : base(parameters) { }

        public async override Task<RequestResult<IEnumerable<MediaDTO>>> Handle(CheckBrandHasMediaQuery request, CancellationToken cancellationToken)
        {
            // Fetch the media entities for the Brand
            var mediaEntities = await _repository.Get(c => c.SourceId == request.ID && c.SourceType == SourceType.Brand).ToListAsync();

            if (!mediaEntities.Any())
            {
                return RequestResult<IEnumerable<MediaDTO>>.Failure(ErrorCode.NotFound);
            }

            var mediaDTOs = mediaEntities.MapList<MediaDTO>();

            return RequestResult<IEnumerable<MediaDTO>>.Success(mediaDTOs);
        }
    }
}
