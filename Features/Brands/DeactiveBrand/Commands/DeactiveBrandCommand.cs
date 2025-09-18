using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.DeactiveBrand.Commands
{
    public record DeactiveBrandCommand(string ID) : IRequestBase<bool>;
    public class DeactiveBrandCommandHandler : RequestHandlerBase<Brand, DeactiveBrandCommand, bool>
    {
        public DeactiveBrandCommandHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveBrandCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Brand brand = new Brand { ID = request.ID };
            brand.IsActive = false;
            _repository.SaveIncluded(brand, nameof(brand.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }

}
