using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.RoleFeatures.DTOs;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.Common.RoleFeatures.Queries
{
    public record GetAllFeaturesListedQuery(int RoleID, string? FeatureName) : IRequestBase<List<GetAllFeaturesListedDTO>>;

    public class GetAllFeaturesListedQueryHandler : RequestHandlerBase<RoleFeature, GetAllFeaturesListedQuery, List<GetAllFeaturesListedDTO>>
    {
        public GetAllFeaturesListedQueryHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<List<GetAllFeaturesListedDTO>>> Handle(GetAllFeaturesListedQuery request, CancellationToken cancellationToken)
        {
            var sectionRanges = new Dictionary<string, (int Start, int End)>
            {
                { "Brand Features", (1, 999) },
                { "Classification Features", (1000, 1999) },
                { "Category Features", (2000, 2999) },
                { "City Features", (3000, 3999) },
                { "Governorate Features", (4000, 4999) },
                { "Product Features", (5000, 5999) },
                { "Role Features", (6000, 6999) },
                { "Client Features", (7000, 7999) },
                { "Client Group Features", (8000, 8999) },
                { "Order Features", (9000, 9999) },
                { "Shipping Address Features", (10000, 10999) },
                { "Discount Features", (11000, 11999) },
                { "Coupon Features", (12000, 12999) },
                { "Advertisement Features", (13000, 13999) },
                { "Media Features", (14000, 14999) },
                { "Company Features", (15000, 15999) },
                { "Company Group Features", (16000, 16999) },
                { "General Features", (17000, int.MaxValue) }
            };

            var allFeatures = Enum.GetValues(typeof(Feature))
                     .Cast<Feature>()
                     .Where(feature => (int)feature != 7022) // Skip Feature 7022
                     .Where(feature => string.IsNullOrWhiteSpace(request.FeatureName) ||
                      feature.ToString().IndexOf(request.FeatureName, StringComparison.OrdinalIgnoreCase) >= 0);

            var groupedFeatures = sectionRanges.Select(section => new GetAllFeaturesListedDTO
            {
                SectionName = section.Key,
                Features = allFeatures
                    .Where(feature => (int)feature >= section.Value.Start && (int)feature <= section.Value.End)
                    .Select(feature => new RoleActiveFeatuersDTO
                    {
                        FeatureId = (int)feature,
                        IsActiveToRole = (_mediator.Send(new CheckIsFeatureAssignedToRoleQuery((int)feature, request.RoleID))).Result.Data
                    }).ToList()
            }).Where(dto => dto.Features.Any())
              .ToList();

            return RequestResult<List<GetAllFeaturesListedDTO>>.Success(groupedFeatures);
        }

    }
}