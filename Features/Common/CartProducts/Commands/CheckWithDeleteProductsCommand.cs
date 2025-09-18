using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Models.CartProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Common.CartProducts.Commands
{
    public record CheckWithDeleteProductsCommand(IEnumerable<GetProductIdsByCompanyIdDTO> IDs):IRequestBase<bool>;
    public class CheckWithDeleteProductsCommandHandler : RequestHandlerBase<CartProduct, CheckWithDeleteProductsCommand, bool>
    {
        public CheckWithDeleteProductsCommandHandler(RequestHandlerBaseParameters<CartProduct> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckWithDeleteProductsCommand request, CancellationToken cancellationToken)
        {
            var productsToDelete = new List<CartProduct>();
            foreach (var item in request.IDs)
            {
                var exist = await _repository.Get(cp => cp.ProductId == item.ID).ToListAsync();
                if(!exist.IsNullOrEmpty())
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
