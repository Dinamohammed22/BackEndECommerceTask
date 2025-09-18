using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.BulkDeactivateAdvertisement.Orchisterators
{
    public record BulkDeactivateAdvertisementOrchisterator(List<string> IDs):IRequestBase<bool>;
    public class BulkDeactivateAdvertisementOrchisteratorHandler : RequestHandlerBase<Advertisement, BulkDeactivateAdvertisementOrchisterator, bool>
    {
        public BulkDeactivateAdvertisementOrchisteratorHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeactivateAdvertisementOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.IDs)
            {
                var result = await _mediator.Send(new DeactivateAdvertisementCommand(id));
                if (!result.IsSuccess)
                {
                    return RequestResult<bool>.Failure(result.ErrorCode);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
