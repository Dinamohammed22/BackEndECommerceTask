using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.EditAdvertisement.Commands
{
    public record EditAdvertisementCommand(string ID,string Title, bool IsActive, ImageType ImageTypes, string? Hyperlink, DateTime StartDate,
        DateTime EndDate) :IRequestBase<bool>;
    public class EditAdvertisementCommandHandler : RequestHandlerBase<Advertisement, EditAdvertisementCommand, bool>
    {
        public EditAdvertisementCommandHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<RequestResult<bool>> Handle(EditAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var check=_repository.Any(a=>a.ID==request.ID);
            if (check)
            {
                Advertisement advertisement = new Advertisement
                {
                    ID = request.ID
                };
                advertisement.Title = request.Title;
                advertisement.IsActive = request.IsActive;
                advertisement.ImageTypes = request.ImageTypes;
                advertisement.Hyperlink = request.Hyperlink;
                advertisement.StartDate = request.StartDate;
                advertisement.EndDate = request.EndDate;

                _repository.SaveIncluded(advertisement, nameof(advertisement.Title), nameof(advertisement.IsActive)
                    , nameof(advertisement.ImageTypes),nameof(advertisement.Hyperlink),nameof(advertisement.StartDate),nameof(advertisement.EndDate));

                _repository.SaveChanges();
                return RequestResult<bool>.Success(true);
            }
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        }
    }
}
