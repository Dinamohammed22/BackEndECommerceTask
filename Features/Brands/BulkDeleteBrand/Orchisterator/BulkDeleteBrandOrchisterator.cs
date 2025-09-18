using KOG.ECommerce.Features.Brands.DeleteBrand.Commands;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.BulkDeleteBrand.Orchisterator
{
    public record BulkDeleteBrandOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkDeleteBrandOrchisteratorHandler : RequestHandlerBase<Brand, BulkDeleteBrandOrchisterator, bool>
    {
        public BulkDeleteBrandOrchisteratorHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteBrandOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeleteBrandCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
                }
            }
            return RequestResult<bool>.Success(true);

        }
    }
}
