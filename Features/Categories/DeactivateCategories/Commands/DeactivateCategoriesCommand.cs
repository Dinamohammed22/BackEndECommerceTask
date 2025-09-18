using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Categories.ActivateCategories.Commands;
using KOG.ECommerce.Models.Categories;

namespace KOG.ECommerce.Features.Categories.DeactivateCategories.Commands
{
    public record DeactivateCategoriesCommand(string Id) : IRequestBase<bool>;
    public class DeactivateCategoriesCommandHandler : RequestHandlerBase<Category, DeactivateCategoriesCommand, bool>
    {
        public DeactivateCategoriesCommandHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactivateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Category category = new Category { ID = request.Id };
            category.IsActive = false;
            _repository.SaveIncluded(category, nameof(category.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);

        }
    }
}
