using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Products.CreateProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Products;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;

namespace KOG.ECommerce.Features.Products.CreateProduct.Orchestrators;

public record CreateProductOrchestrator(string Name, string Description, string CategoryId, List<string> Tags,
    string Model, double Price, double Tax, string BrandId,
    int MinimumQuantity, int MaximumQuantity, double Length,
    double Width, double Height, double Liter, DateTime AvailableDate,
    List<string> Paths, string SpecificationMetrix, string Data, bool FeaturedProduct,
    int Quantity, int NumberOfPoints, bool IsActivePoint, bool IsActive, Grade Grade) : IRequestBase<bool>;
public class CreateProductOrchestratorHandler : RequestHandlerBase<Product, CreateProductOrchestrator, bool>
{
    public CreateProductOrchestratorHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(CreateProductOrchestrator request, CancellationToken cancellationToken)
    {
        var ProductId = await _mediator.Send(request.MapOne<CreateProductCommand>());
        var result = await _mediator.Send(new SaveMediaCommand(
       SourceId: ProductId.Data,
       SourceType: SourceType.Product,
       Paths: request.Paths
   ), cancellationToken);
        return await Task.FromResult(result);
    }
    
}

