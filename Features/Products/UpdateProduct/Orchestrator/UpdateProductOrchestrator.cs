using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.Products.CreateProduct.Commands;
using KOG.ECommerce.Features.Products.UpdateProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Products.UpdateProduct.Orchestrator;

public record UpdateProductOrchestrator(string ID, string Name, string Description, string CategoryId, List<string> Tags,
    string Model, double Price, double Tax, string BrandId,
    int MinimumQuantity, int MaximumQuantity, double Length,
    double Width, double Height, double Liter, DateTime AvailableDate, 
    List<string>? Paths, string SpecificationMetrix, string Data, bool FeaturedProduct, 
    int Quantity, int NumberOfPoints, bool IsActive, bool IsActivePoint, Grade Grade) : IRequestBase<bool>;

public class UpdateProductOrchestratorHandler : RequestHandlerBase<Product, UpdateProductOrchestrator, bool>
{
    public UpdateProductOrchestratorHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(UpdateProductOrchestrator request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request.MapOne<UpdateProductCommand>());
        if (!request.Paths.IsNullOrEmpty())
        {
            var result = await _mediator.Send(new SaveMediaCommand(
       SourceId: request.ID,
       SourceType: SourceType.Product,
       Paths: request.Paths
   ), cancellationToken);
        }
        return RequestResult<bool>.Success(true);
    }
}