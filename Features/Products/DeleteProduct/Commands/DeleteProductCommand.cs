using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.DeleteProduct.Commands;

public record DeleteProductCommand(string ID) : IRequestBase<bool>;
public class DeleteProductCommandHandler:RequestHandlerBase<Product, DeleteProductCommand,bool>
{
    public DeleteProductCommandHandler(RequestHandlerBaseParameters<Product> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
       var check = await _repository.AnyAsync(p => p.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        var checkResult = await _mediator.Send(request.MapOne<CheckIfProductInOrderQuery>());
        if (!checkResult.Data)
        {
            Product product = new Product();
            product.ID = request.ID;
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return RequestResult<bool>.Success(true);
        }
       return RequestResult<bool>.Failure(ErrorCode.CannotDelete);
    }
}

