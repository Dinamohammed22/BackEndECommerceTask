using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;
using KOG.ECommerce.Features.Categories.DeactivateCategories.Commands;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.BulkDeActivateCategory.Orchisterator
{
    public record BulkDeActivateCategoryOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkDeActivateCategoryOrchisteratorHandler : RequestHandlerBase<Category, BulkDeActivateCategoryOrchisterator, bool>
    {
        public BulkDeActivateCategoryOrchisteratorHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkDeActivateCategoryOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new DeactivateCategoriesCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
