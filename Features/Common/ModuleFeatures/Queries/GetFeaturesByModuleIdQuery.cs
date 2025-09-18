using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.ModuleFeatures.DTOs;
using KOG.ECommerce.Models.ModuleFeatures;


namespace KOG.ECommerce.Features.Common.ModuleFeatures.Queries
{
    public record GetFeaturesByModuleIdQuery(string ModuleId) : IRequestBase<IEnumerable<GetFeaturesByModuleIdDTO>>;

    public class GetFeaturesByModuleIdHandler : RequestHandlerBase<ModuleFeature, GetFeaturesByModuleIdQuery, IEnumerable<GetFeaturesByModuleIdDTO>>
    {
        public GetFeaturesByModuleIdHandler(RequestHandlerBaseParameters<ModuleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetFeaturesByModuleIdDTO>>> Handle(GetFeaturesByModuleIdQuery request, CancellationToken cancellationToken)
        {
            var features = _repository
               .Get(f => f.ModuleId == request.ModuleId)
                .Select(c => new GetFeaturesByModuleIdDTO(c.Features)) 
                .ToList();


            return RequestResult<IEnumerable<GetFeaturesByModuleIdDTO>>.Success(features);
        }
    }
}
