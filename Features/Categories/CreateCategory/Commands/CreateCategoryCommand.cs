using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.CreateCategory.Commands;

public record CreateCategoryCommand(string Name, string Description, string? ParentCategoryId, List<string> Tags, List<string> SEO, bool IsActive) : IRequestBase<string>;

public class CreateCategoryCommandHandler : RequestHandlerBase<Category, CreateCategoryCommand, string>
{
    public CreateCategoryCommandHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters) { }

    public async override Task<RequestResult<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category { Name = request.Name, ParentCategoryId = request.ParentCategoryId,
            Tags = request.Tags,Description=request.Description,SEO=request.SEO,IsActive=request.IsActive };
        _repository.Add(category);
        _repository.SaveChanges();
        return await Task.FromResult(RequestResult<string>.Success(category.ID));
    }
}
