using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;

namespace KOG.ECommerce.Features.Categories.BulkActivateCategory.Orchisterator
{
    public record BulkActivateCategoryOrchisterator(List<string> Ids) : IRequestBase<bool>;

    public class BulkActivateCategoryOrchisteratorHandler : RequestHandlerBase<Category, BulkActivateCategoryOrchisterator, bool>
    {
        public BulkActivateCategoryOrchisteratorHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters) { }

        public async override Task<RequestResult<bool>> Handle(BulkActivateCategoryOrchisterator request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var result = await _mediator.Send(new ActivateCategoriesCommand(id));
                if (!result.Data)
                {
                    return RequestResult<bool>.Failure(ErrorCode.NotFound);
                }
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
