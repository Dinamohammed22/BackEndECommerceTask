using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Users;

namespace KOG.ECommerce.Features.Users.GetVerifyStatusList.Queries
{
    public record GetVerifyStatusListQuery() : IRequestBase<List<SelectListItemViewModel>>;
    public class GetVerifyStatusListQueryHandler : RequestHandlerBase<User, GetVerifyStatusListQuery, List<SelectListItemViewModel>>
    {
        public GetVerifyStatusListQueryHandler(RequestHandlerBaseParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<List<SelectListItemViewModel>>> Handle(GetVerifyStatusListQuery request, CancellationToken cancellationToken)
        {
            var verifyStatusList = EnumHelper.ToSelectableList<VerifyStatus>();

            return RequestResult<List<SelectListItemViewModel>>.Success(verifyStatusList);
        }
    }
}