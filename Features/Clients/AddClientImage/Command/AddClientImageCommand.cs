using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Clients.AddClientImage.Command
{
    public record AddClientImageCommand(List<string> Paths):IRequestBase<bool>;
    public class AddClientImageCommandHandler : RequestHandlerBase<Media, AddClientImageCommand, bool>
    {
        public AddClientImageCommandHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddClientImageCommand request, CancellationToken cancellationToken)
        {
            if (request.Paths != null && request.Paths.Any())
            {
                var clientId = _userState.UserID;

                var existingMedia = _repository
                    .Get(m => m.SourceId == clientId && m.SourceType == SourceType.Client && !m.Deleted)
                    .ToList();

                if (existingMedia.Any())
                {
                    foreach (var media in existingMedia)
                    {
                        _repository.Update(media);
                    }

                     _repository.SaveChanges();
                }

                var saveMediaResult = await _mediator.Send(new SaveMediaCommand(
                    SourceId: clientId,
                    SourceType: SourceType.Client,
                    Paths: request.Paths
                ));

                if (!saveMediaResult.IsSuccess)
                    return RequestResult<bool>.Failure(saveMediaResult.ErrorCode);

                return RequestResult<bool>.Success(true);
            }

            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        }
    }
}
