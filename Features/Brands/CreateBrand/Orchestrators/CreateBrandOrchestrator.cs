using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Brands.CreateBrand.Commands;
using KOG.ECommerce.Features.Categories.CreateCategory.Commands;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Brands.CreateBrand.Orchestrators
{
    public record CreateBrandOrchestrator(string Name, List<string> Tags, List<string>? Paths, bool IsActive) : IRequestBase<string>;
    public class CreateBrandOrchestratorHandler : RequestHandlerBase<Brand, CreateBrandOrchestrator, string>
    {
        public CreateBrandOrchestratorHandler(RequestHandlerBaseParameters<Brand> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(CreateBrandOrchestrator request, CancellationToken cancellationToken)
        {
            var brandResult = await _mediator.Send(request.MapOne<CreateBrandCommand>());

            if (!brandResult.IsSuccess)
            {
                return RequestResult<string>.Failure(brandResult.ErrorCode);
            }

            if (request.Paths != null && request.Paths.Any())
            {
                var saveMediaResult = await _mediator.Send(new SaveMediaCommand(
                    SourceId: brandResult.Data,
                    SourceType: SourceType.Brand,
                    Paths: request.Paths
                ), cancellationToken);


                if (!saveMediaResult.IsSuccess)
                {

                    return RequestResult<string>.Failure(saveMediaResult.ErrorCode);
                }
            }

            return RequestResult<string>.Success(brandResult.Data);
        }
    }

}
