using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Categories.EditCategory.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.Products.UpdateProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Categories.EditCategory.Orchestrators
{
    public record EditCategoryOrchestrator(string Id, string Name, string Description, string? ParentCategoryId, 
        List<string> Tags, List<string> SEO, bool IsActive, List<string>? Paths) : IRequestBase<bool>;
    public class EditCategoryOrchestratorHandler : RequestHandlerBase<Category, EditCategoryOrchestrator, bool>
    {
        public EditCategoryOrchestratorHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditCategoryOrchestrator request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request.MapOne<EditCategoryCommand>());
            if (!request.Paths.IsNullOrEmpty())
            {
                var result = await _mediator.Send(new SaveMediaCommand(
           SourceId: request.Id,
           SourceType: SourceType.Category,
           Paths: request.Paths
       ), cancellationToken);
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
