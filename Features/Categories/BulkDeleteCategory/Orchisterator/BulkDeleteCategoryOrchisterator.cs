using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Brands.DeactiveBrand.Commands;
using KOG.ECommerce.Features.Categories.DeleteCategory.Command;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.BulkDeleteCategory.Orchisterator
{
    public record BulkDeleteCategoryOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkDeleteCategoryOrchisteratorHandler : RequestHandlerBase<Category, BulkDeleteCategoryOrchisterator, bool>
    {
        public BulkDeleteCategoryOrchisteratorHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeleteCategoryOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeleteCategoryCammand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
