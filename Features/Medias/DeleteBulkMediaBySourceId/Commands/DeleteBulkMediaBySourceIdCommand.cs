using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Medias.DeleteBulkMediaBySourceId.Commands
{
    public record DeleteBulkMediaBySourceIdCommand(string SourceId) : IRequestBase<bool>;

    public class DeleteBulkMediaBySourceIdCommandHandler : RequestHandlerBase<Media, DeleteBulkMediaBySourceIdCommand, bool>
    {
        public DeleteBulkMediaBySourceIdCommandHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(DeleteBulkMediaBySourceIdCommand request, CancellationToken cancellationToken)
        {
            if (request.SourceId == null)
            {
                return RequestResult<bool>.Failure();
            }

            var mediasToDelete = _repository.Get(m=> m.SourceId == request.SourceId).ToList();

            if (!mediasToDelete.Any())
            {
                return RequestResult<bool>.Failure();
            }

            foreach (var media in mediasToDelete)
            {
                // Delete file from file system
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", media.Path);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        File.Delete(fullPath);
                    }
                    catch (Exception ex)
                    {
                        return RequestResult<bool>.Failure();
                    }
                }

                // Soft delete from database
                //_repository.Delete(media);
            }

            _repository.BulkHardDelete(m => m.SourceId == request.SourceId);

            _repository.SaveChanges();

            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
