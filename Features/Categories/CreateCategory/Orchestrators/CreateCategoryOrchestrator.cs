using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Categories.CreateCategory.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Categories.CreateCategory.Orchestrators
{
    public record CreateCategoryOrchestrator(string Name, string Description, string? ParentCategoryId, List<string> Tags, List<string> SEO, List<string> Paths, bool IsActive) : IRequestBase<string>;

    public class CreateCategoryOrchestratorHandler : RequestHandlerBase<Category, CreateCategoryOrchestrator, string>
    {
        public CreateCategoryOrchestratorHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateCategoryOrchestrator request, CancellationToken cancellationToken)
        {
            var categoryResult = await _mediator.Send(request.MapOne<CreateCategoryCommand>());

            if (!categoryResult.IsSuccess)
            {
                return RequestResult<string>.Failure(categoryResult.ErrorCode);
            }

            if (request.Paths != null && request.Paths.Any())
            {
                var saveMediaResult = await _mediator.Send(new SaveMediaCommand(
                    SourceId: categoryResult.Data, 
                    SourceType: SourceType.Category,
                    Paths: request.Paths
                ), cancellationToken);

       
                if (!saveMediaResult.IsSuccess)
                {
           
                    return RequestResult<string>.Failure(saveMediaResult.ErrorCode);
                }
            }

            return RequestResult<string>.Success(categoryResult.Data);
        }
    }
}
