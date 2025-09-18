using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Medias.SaveMedia.Commands
{
    public record SaveMediaCommand(string SourceId, SourceType SourceType, List<string> Paths) : IRequestBase<bool>;

    public class SaveMediaCommandHandler : RequestHandlerBase<Media, SaveMediaCommand, bool>
    {
        public SaveMediaCommandHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(SaveMediaCommand request, CancellationToken cancellationToken)
        {
            var existingMedias = _repository.Get(media => media.SourceId == request.SourceId && media.SourceType == request.SourceType).ToList();
            foreach (var media in existingMedias)
            {
                _repository.Delete(media);
            }


            var medias = request.Paths.Select(path => new Media
            {
                Path = path,
                SourceId = request.SourceId,
                SourceType = request.SourceType
            }).ToList();

            _repository.AddRange(medias);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }

}