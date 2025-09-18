using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.DeleteAdvertisement.Commands
{
    public record DeleteAdvertisementCommand(string ID) : IRequestBase<bool>;

    public class DeleteAdvertisementCommandHandler : RequestHandlerBase<Advertisement, DeleteAdvertisementCommand, bool>
    {
        public DeleteAdvertisementCommandHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));

        }
    }
}
