using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.OrderItems;

namespace KOG.ECommerce.Features.Common.OrderItems.Queries
{
    public record CheckIfProductInOrderQuery(string ID):IRequestBase<bool>;
    public class CheckIfProductInOrderQueryHandler : RequestHandlerBase<OrderItem, CheckIfProductInOrderQuery, bool>
    {
        public CheckIfProductInOrderQueryHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(CheckIfProductInOrderQuery request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(o => o.ProductId == request.ID);
            return RequestResult<bool>.Success(check);
        }
    }
}
