using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Brands.EditBrand.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.Products.UpdateProduct.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Brands.EditBrand.Orchestrators
{
    public record EditBrandOrchestrator(string ID, string Name, List<string> Tags, List<string>? Paths, bool IsActive) : IRequestBase<bool>;
    public class EditBrandOrchestratorHandler : RequestHandlerBase<Brand, EditBrandOrchestrator, bool>
    {
        public EditBrandOrchestratorHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(EditBrandOrchestrator request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request.MapOne<EditBrandCommand>());
            if (!request.Paths.IsNullOrEmpty())
            {
                var result = await _mediator.Send(new SaveMediaCommand(
               SourceId: request.ID,
               SourceType: SourceType.Brand,
               Paths: request.Paths
              ));
            }
            return RequestResult<bool>.Success(true);
        }
    }

}
