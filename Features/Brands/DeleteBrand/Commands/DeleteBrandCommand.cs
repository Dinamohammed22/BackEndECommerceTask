using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;

namespace KOG.ECommerce.Features.Brands.DeleteBrand.Commands
{
    public record DeleteBrandCommand(string ID) : IRequestBase<bool>;

    public class DeleteBrandCommandHandler : RequestHandlerBase<Brand, DeleteBrandCommand, bool>
    {
        public DeleteBrandCommandHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters) { }

        public async override Task<RequestResult<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            var checkResult = await _mediator.Send(request.MapOne<CheckIfProductHasBrandQuery>());
            if (!checkResult.Data)
            {
                _repository.Delete(request.ID);
                _repository.SaveChanges();
                return await Task.FromResult(RequestResult<bool>.Success(true));
            }
            var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

            return await Task.FromResult(result);

        }
    }

}
