using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.Common.RoleFeatures.Queries
{
    public record SearchFeatureByNameQuery(string? FeatureName) :IRequestBase<IEnumerable<Feature>>;
    public class SearchFeatureByNameQueryHandler : RequestHandlerBase<RoleFeature, SearchFeatureByNameQuery, IEnumerable<Feature>>
    {
        public SearchFeatureByNameQueryHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<RequestResult<IEnumerable<Feature>>> Handle(SearchFeatureByNameQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FeatureName))
            {
                return RequestResult<IEnumerable<Feature>>.Success(Enumerable.Empty<Feature>());
            }

            IEnumerable<Feature> Featuers = EnumHelper.SearchEnumByName<Feature>(request.FeatureName);
            return RequestResult<IEnumerable<Feature>>.Success(Featuers);
        }
    }
}
