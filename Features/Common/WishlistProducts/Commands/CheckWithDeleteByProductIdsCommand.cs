using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.WishlistProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.WishlistProducts.Commands
{
    public record CheckWithDeleteByProductIdsCommand(IEnumerable<GetProductIdsByCompanyIdDTO> IDs) : IRequestBase<bool>;
    public class CheckWithDeleteByProductIdsCommandHandler : RequestHandlerBase<WishlistProduct, CheckWithDeleteByProductIdsCommand, bool>
    {
        public CheckWithDeleteByProductIdsCommandHandler(RequestHandlerBaseParameters<WishlistProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckWithDeleteByProductIdsCommand request, CancellationToken cancellationToken)
        {
            var productsToDelete = new List<WishlistProduct>();
            foreach (var item in request.IDs)
            {
                var exist = await _repository.Get(cp => cp.ProductId == item.ID).ToListAsync();
                if (!exist.IsNullOrEmpty())
                {
                    productsToDelete.AddRange(exist);

                }
            }
            foreach (var product in productsToDelete)
            {
                _repository.Delete(product);
                _repository.SaveChanges();
            }
            return RequestResult<bool>.Success(true);
        }
    }
}
