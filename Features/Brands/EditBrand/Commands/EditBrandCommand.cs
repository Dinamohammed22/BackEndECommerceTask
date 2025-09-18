using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Governorates.UpdateGovernorate.Commands;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Brands.EditBrand.Commands
{
    public record EditBrandCommand(string ID, string Name, List<string> Tags, bool IsActive) : IRequestBase<bool>;

    public class EditBrandCommandHandler : RequestHandlerBase<Brand, EditBrandCommand, bool>
    {
        public EditBrandCommandHandler(RequestHandlerBaseParameters<Brand> parameters) : base(parameters)
        { }

        public async override Task<RequestResult<bool>> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Brand brand = new Brand { ID = request.ID };
            brand.Name = request.Name;
            brand.Tags=request.Tags;
            brand.IsActive=request.IsActive;
            _repository.SaveIncluded(brand, nameof(brand.Name),nameof(brand.Tags),nameof(brand.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);

            return await Task.FromResult(result);
        }
    }

}
