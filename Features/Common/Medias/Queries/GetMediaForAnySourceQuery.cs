using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.Medias.Queries
{
    public record GetMediaForAnySourceQuery(string SourceId, SourceType SourceType):IRequestBase<string>;
    public class GetMediaForAnySourceQueryHandler : RequestHandlerBase<Media, GetMediaForAnySourceQuery, string>
    {
        public GetMediaForAnySourceQueryHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(GetMediaForAnySourceQuery request, CancellationToken cancellationToken)
        {
            string Path=await _repository.Get(m=>m.SourceId==request.SourceId && m.SourceType==request.SourceType).Select(m => m.Path).FirstOrDefaultAsync();
            if (Path.IsNullOrEmpty())
                return RequestResult<string>.Failure(ErrorCode.NotFound);
            return RequestResult<string>.Success(Path);
        }
    }
}
