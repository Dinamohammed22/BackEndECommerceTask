using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Cities.CreateCity.Commands;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Commands
{
    public record CreateAdvertisementCommand(string Title,
        bool IsActive,
        ImageType ImageTypes,
        List<string> Paths,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate) : IRequestBase<string>;

    public class CreateAdvertisementCommandHandler : RequestHandlerBase<Advertisement, CreateAdvertisementCommand, string>
    {
        public CreateAdvertisementCommandHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            Advertisement advertisement = new Advertisement
            {
                Title = request.Title,
                IsActive = request.IsActive,
                ImageTypes = request.ImageTypes,
                Hyperlink = request.Hyperlink,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            _repository.Add(advertisement);

            _repository.SaveChanges();

            string advertisementId = advertisement.ID;

            return RequestResult<string>.Success(advertisementId);
        }
    }
}
