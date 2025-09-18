using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement.Commands;
using KOG.ECommerce.Features.Advertisements.DeleteAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.BulkDeleteAdvertisement.Orchisterators
{
    public record BulkDeleteAdvertisementOrchisterator(List<string> IDs):IRequestBase<bool>;
    public class BulkDeleteAdvertisementOrchisteratorHandler : RequestHandlerBase<Advertisement, BulkDeleteAdvertisementOrchisterator, bool>
    {
        public BulkDeleteAdvertisementOrchisteratorHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteAdvertisementOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.IDs)
            {
                var result = await _mediator.Send( new DeleteAdvertisementCommand(id));
                if (!result.IsSuccess)
                {
                    return RequestResult<bool>.Failure(result.ErrorCode);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
