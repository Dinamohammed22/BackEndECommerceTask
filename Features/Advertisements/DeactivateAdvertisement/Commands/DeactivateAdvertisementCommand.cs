using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement.Commands
{
    public record DeactivateAdvertisementCommand(string ID):IRequestBase<bool>;
    public class DeactivateAdvertisementCommandHandler : RequestHandlerBase<Advertisement, DeactivateAdvertisementCommand, bool>
    {
        public DeactivateAdvertisementCommandHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactivateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(ad => ad.ID == request.ID);
            if (check)
            {
                var advertsement = new Advertisement
                {
                    ID = request.ID,
                    IsActive = false
                };
                _repository.SaveIncluded(advertsement, nameof(advertsement.IsActive));
                _repository.SaveChanges();
                return (RequestResult<bool>.Success(true));
            }

            return (RequestResult<bool>.Failure(ErrorCode.NotFound));
        }
    }
}
