using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Common.Classifications.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Classifications.SelectListClassification
{
    public class SelectListClassificationEndpoint:EndpointBase<SelectListClassificationRequestViewModel,SelectListClassificationResponseViewModel>
    {
        public SelectListClassificationEndpoint(EndpointBaseParameters<SelectListClassificationRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.SelectListClassification })]
        public async Task<EndPointResponse<IEnumerable<SelectListClassificationResponseViewModel>>> SelectListClassification()
        {


            var result = await _mediator.Send(new SelectListClassificationQuery());

            var response = result.Data.MapList<SelectListClassificationResponseViewModel>();

            if (result.IsSuccess)
                return EndPointResponse<IEnumerable<SelectListClassificationResponseViewModel>>.Success(response, "Classification filtered successfully.");
            else
                return EndPointResponse<IEnumerable<SelectListClassificationResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
