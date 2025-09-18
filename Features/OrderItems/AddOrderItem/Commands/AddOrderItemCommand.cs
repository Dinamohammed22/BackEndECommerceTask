using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.OrderItems;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.OrderItems.AddOrderItem.Commands
{
    public record AddOrderItemsCommand(List<OrderItem> Items) : IRequestBase<bool>;

    public class AddOrderItemCommandHandler : RequestHandlerBase<OrderItem, AddOrderItemsCommand, bool>
    {
        public AddOrderItemCommandHandler(RequestHandlerBaseParameters<OrderItem> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddOrderItemsCommand request, CancellationToken cancellationToken)
        {
            _repository.AddRange(request.Items); 
            _repository.SaveChanges();

            return RequestResult<bool>.Success(true);
        }
    }

}
