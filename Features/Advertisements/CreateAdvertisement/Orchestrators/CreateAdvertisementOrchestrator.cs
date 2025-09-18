using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.Products.CreateProduct.Commands;
using KOG.ECommerce.Features.Products.CreateProduct.Orchestrators;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Orchestrators
{
    public record CreateAdvertisementOrchestrator(
      string Title,
        bool IsActive,
        ImageType ImageTypes,
        List<string> Paths,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequestBase<bool>;
    public class CreateAdvertisementOrchestratorHandler : RequestHandlerBase<Advertisement, CreateAdvertisementOrchestrator, bool>
    {
        public CreateAdvertisementOrchestratorHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CreateAdvertisementOrchestrator request, CancellationToken cancellationToken)
        {
            var advertisementId = await _mediator.Send(request.MapOne<CreateAdvertisementCommand>());

            var result = await _mediator.Send(new SaveMediaCommand(
                SourceId: advertisementId.Data,
                SourceType: SourceType.Advertisement,
                Paths: request.Paths
                ));

            return await Task.FromResult(result);
        }

    }
}
