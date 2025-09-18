using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Medias.DeleteMedia.Commands
{
    public record DeleteMediaCommand(string ID) : IRequestBase<bool>;

    public class DeleteMediaCommandHandler : RequestHandlerBase<Media, DeleteMediaCommand, bool>
    {
        public DeleteMediaCommandHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            if (request.ID == null)
            {
                return RequestResult<bool>.Failure();
            }

            var mediaToDelete = _repository.GetByID(request.ID);

            if (mediaToDelete == null)
            {
                return RequestResult<bool>.Failure();
            }

            // Delete file from file system
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mediaToDelete.Path);
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
            _repository.BulkHardDelete(m => m.ID == request.ID);

            _repository.SaveChanges();

            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
