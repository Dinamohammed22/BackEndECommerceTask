using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.DeleteCategory.Command;

public record DeleteCategoryCammand(string ID) : IRequestBase<bool>;

public class DeleteCategoryCammandHandler : RequestHandlerBase<Category, DeleteCategoryCammand, bool>
{
    public DeleteCategoryCammandHandler(RequestHandlerBaseParameters<Category> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(DeleteCategoryCammand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        var findParentCategoryId = await _repository.AnyAsync(c => c.ParentCategoryId == request.ID);
        var checkProducts = await _mediator.Send(request.MapOne<CheckIfCategoryHasProductsQuery>());
        if (!findParentCategoryId&& !checkProducts.Data)
        {
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
        var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

        return await Task.FromResult(result);
    }
}
