using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.ActiveBrand.Commands
{
    public record ActiveBrandCommand(string ID):IRequestBase<bool>;
    public class ActiveBrandCommandHandler : RequestHandlerBase<Brand, ActiveBrandCommand, bool>
    {
        public ActiveBrandCommandHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveBrandCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.BrandNotFound);
            Brand brand = new Brand { ID = request.ID };
            brand.IsActive = true;
            _repository.SaveIncluded(brand, nameof(brand.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }


}
