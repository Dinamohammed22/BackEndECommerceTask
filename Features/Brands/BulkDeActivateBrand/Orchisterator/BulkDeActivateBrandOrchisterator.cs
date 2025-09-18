using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Brands.ActiveBrand.Commands;
using KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator;
using KOG.ECommerce.Features.Brands.DeactiveBrand.Commands;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.BulkDeActivateBrand.Orchisterator
{
    public record BulkDeActivateBrandOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkDeActivateBrandOrchisteratorHandler : RequestHandlerBase<Brand, BulkDeActivateBrandOrchisterator, bool>
    {
        public BulkDeActivateBrandOrchisteratorHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeActivateBrandOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeactiveBrandCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);

        }
    }
}
