using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.EditCategory.Commands
{
    public record EditCategoryCommand(string Id, string Name, string Description, string? ParentCategoryId, List<string> Tags, List<string> SEO, bool IsActive) : IRequestBase<bool>;

    public class EditCategoryCommandHandler : RequestHandlerBase<Category, EditCategoryCommand, bool>
    {
        public EditCategoryCommandHandler(RequestHandlerBaseParameters<Category> requestParameters)
            : base(requestParameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIDAsync(request.Id);
            if (category==null)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentCategoryId = string.IsNullOrWhiteSpace(request.ParentCategoryId)
                ? null
                : request.ParentCategoryId;

            category.Tags.Clear();
            category.Tags.AddRange(request.Tags);

            category.SEO.Clear();
            category.SEO.AddRange(request.SEO);
            category.IsActive = request.IsActive;
            _repository.SaveIncluded(category,
                  nameof(category.Name), nameof(category.ParentCategoryId), nameof(category.Tags),
                   nameof(category.Description), nameof(category.SEO),nameof(category.IsActive));

            _repository.SaveChanges();

            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
