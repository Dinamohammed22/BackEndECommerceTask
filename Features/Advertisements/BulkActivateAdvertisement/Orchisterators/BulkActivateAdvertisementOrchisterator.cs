using FluentValidation;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Advertisements.ActiveAdvertisement.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;

namespace KOG.ECommerce.Features.Advertisements.BulkActivateAdvertisement.Orchisterators
{
    public record BulkActivateAdvertisementOrchisterator(List<string> IDs):IRequestBase<bool>;
    public class BulkActivateAdvertisementOrchisteratorHandler : RequestHandlerBase<Advertisement, BulkActivateAdvertisementOrchisterator, bool>
    {
        public BulkActivateAdvertisementOrchisteratorHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(BulkActivateAdvertisementOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.IDs)
            {
                var result = await _mediator.Send(new ActiveAdvertisementCommand(id));
                if (!result.IsSuccess)
                {
                    return RequestResult<bool>.Failure(result.ErrorCode);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
