using AutoMapper;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.RoleFeatures.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.RoleFeatures;

namespace KOG.ECommerce.Features.Common.RoleFeatures.GetFeaturesByRoleId
{
    public record GetFeaturesByRoleIdQuery(Role? RoleId) : IRequestBase<List<int>>;

    public class GetFeaturesByRoleIdQueryHandler : RequestHandlerBase<RoleFeature, GetFeaturesByRoleIdQuery, List<int>>
    {

        public GetFeaturesByRoleIdQueryHandler(RequestHandlerBaseParameters<RoleFeature> requestParameters, IMapper mapper)
            : base(requestParameters)
        {
        }

        public override async Task<RequestResult<List<int>>> Handle(GetFeaturesByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var roleId = request.RoleId ?? _userState.RoleID;
            
            var featureEnums = _repository
                .Get(rf => rf.RoleId == roleId)
                .Select(rf => ((int)rf.Features))
                .ToList();

            //var featureDtos = featureEnums.MapList<GetFeaturesByRoleIdProfileDTO>();

            return RequestResult<List<int>>.Success(featureEnums);
        }
    }

}
