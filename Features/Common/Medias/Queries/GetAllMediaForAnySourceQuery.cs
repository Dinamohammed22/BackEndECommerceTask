using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Medias.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Common.Medias.Queries
{
    public record GetAllMediaForAnySourceQuery(string SourceId, SourceType SourceType) : IRequestBase<IEnumerable<MediaDTO>>;
    public class GetAllMediaForAnySourceQueryHandler : RequestHandlerBase<Media, GetAllMediaForAnySourceQuery, IEnumerable<MediaDTO>>
    {
        public GetAllMediaForAnySourceQueryHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<MediaDTO>>> Handle(GetAllMediaForAnySourceQuery request, CancellationToken cancellationToken)
        {
            var mediaEntities = _repository.Get(c => c.SourceId == request.SourceId && c.SourceType == request.SourceType).ToList();
            if (mediaEntities is null)
            {
                return RequestResult<IEnumerable<MediaDTO>>.Failure(ErrorCode.MediaNotFound);
            }
            var mediaDTO = mediaEntities.MapList<MediaDTO>();

            return RequestResult<IEnumerable<MediaDTO>>.Success(mediaDTO);
        }
    }
}
