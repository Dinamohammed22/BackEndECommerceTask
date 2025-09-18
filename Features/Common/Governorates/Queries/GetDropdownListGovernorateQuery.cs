using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record GetDropdownListGovernorateQuery : IRequestBase<IEnumerable<SelectListItemViewModel>>;
    public class GetDropdownListGovernorateQueryHandler : RequestHandlerBase<Governorate, GetDropdownListGovernorateQuery, IEnumerable<SelectListItemViewModel>>
    {
        public GetDropdownListGovernorateQueryHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<SelectListItemViewModel>>> Handle(GetDropdownListGovernorateQuery request, CancellationToken cancellationToken)
        {

            var selectListItems = _repository.Get().ToSelectListViewModel();
            return RequestResult<IEnumerable<SelectListItemViewModel>>.Success(selectListItems);

        }
    }


}
