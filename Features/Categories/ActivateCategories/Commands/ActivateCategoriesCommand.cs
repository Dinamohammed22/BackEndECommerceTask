using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Categories;
using MediatR.Wrappers;

namespace KOG.ECommerce.Features.Categories.ActivateCategories.Commands
{
    public record ActivateCategoriesCommand(string Id) : IRequestBase<bool>;
    public class ActivateCategoriesCommandHandler : RequestHandlerBase<Category, ActivateCategoriesCommand, bool>
    {
        public ActivateCategoriesCommandHandler(RequestHandlerBaseParameters<Category> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActivateCategoriesCommand request, CancellationToken cancellationToken)
        {
            
            var check = await _repository.AnyAsync(b => b.ID == request.Id);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Category category=new Category { ID = request.Id };
            category.IsActive = true;
            _repository.SaveIncluded(category, nameof(category.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);

        }
    }
}
