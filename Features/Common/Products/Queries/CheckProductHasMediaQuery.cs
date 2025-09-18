using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KOG.ECommerce.Features.Common.Products.Queries
{
    public record CheckProductHasMediaQuery(string ID) : IRequestBase<IEnumerable<MediaDTO>>;

    public class CheckProductHasMediaQueryHandler : RequestHandlerBase<Media, CheckProductHasMediaQuery, IEnumerable<MediaDTO>>
    {
        public CheckProductHasMediaQueryHandler(RequestHandlerBaseParameters<Media> parameters) : base(parameters) { }

        public async override Task<RequestResult<IEnumerable<MediaDTO>>> Handle(CheckProductHasMediaQuery request, CancellationToken cancellationToken)
        {
            // Fetch the media entities for the product
            var mediaEntities = await _repository.Get(c => c.SourceId == request.ID && c.SourceType == SourceType.Product)
                .ToListAsync();

            if (!mediaEntities.Any())
            {
                return RequestResult<IEnumerable<MediaDTO>>.Failure(ErrorCode.NotFound);
            }

            var mediaDTOs = mediaEntities.MapList<MediaDTO>();

            return RequestResult<IEnumerable<MediaDTO>>.Success(mediaDTOs);
        }
    }
}
