using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Commands;
using KOG.ECommerce.Features.Advertisements.EditAdvertisement.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Advertisements.EditAdvertisement.Orchestrators
{
    public record EditAdvertisementOrchestrtor(
        string ID, 
        string Title,
        bool IsActive,
        ImageType ImageTypes,
        List<string>? Paths,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate) :IRequestBase<bool>;
    public class EditAdvertisementOrchestrtorHandler : RequestHandlerBase<Advertisement, EditAdvertisementOrchestrtor, bool>
    {
        public EditAdvertisementOrchestrtorHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditAdvertisementOrchestrtor request, CancellationToken cancellationToken)
        {
            var advertisementId = await _mediator.Send(request.MapOne<EditAdvertisementCommand>());
            if (!request.Paths.IsNullOrEmpty())
            {
                var result = await _mediator.Send(new SaveMediaCommand(
                SourceId: request.ID,
                SourceType: SourceType.Advertisement,
                Paths: request.Paths
                ));
            }
            return RequestResult<bool>.Success(true);
        }
    }

}
