using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Brands.ActiveBrand.Commands;
using KOG.ECommerce.Features.Brands.BulkDeleteBrand.Orchisterator;
using KOG.ECommerce.Features.Brands.DeleteBrand.Commands;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.BulkActivateBrand.Orchisterator
{
    public record BulkActivateBrandOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkActivateBrandOrchisteratorHandler : RequestHandlerBase<Brand, BulkActivateBrandOrchisterator, bool>
    {
        public BulkActivateBrandOrchisteratorHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkActivateBrandOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new ActiveBrandCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);

        }
    }
}
