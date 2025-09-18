using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.ActiveAdvertisement.Commands
{
    public record ActiveAdvertisementCommand(string ID) : IRequestBase<bool>;
    public class ActiveAdvertisementCommandHandler : RequestHandlerBase<Advertisement, ActiveAdvertisementCommand, bool>
    {
        public ActiveAdvertisementCommandHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(ad=> ad.ID == request.ID);
            if (check)
            {
                var advertsement = new Advertisement
                {
                    ID = request.ID,
                    IsActive = true
                };
                _repository.SaveIncluded(advertsement, nameof(advertsement.IsActive));
                _repository.SaveChanges();
                return (RequestResult<bool>.Success(true));
            }

            return (RequestResult<bool>.Failure(ErrorCode.NotFound));
        }
    }
}
