using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Brands.GetBrandByName;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.GetBrandsNames
{
    public class GetBrandsNamesEndPoint : EndpointBase<GetBrandsNamesRequestViewModel, GetBrandsNamesResponseViewModel>
    {
        public GetBrandsNamesEndPoint(EndpointBaseParameters<GetBrandsNamesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetBrandsNames })]
        public async Task<EndPointResponse<IEnumerable<GetBrandsNamesResponseViewModel>>> GetBrandsNames([FromQuery] GetBrandsNamesRequestViewModel viewModel)
        {
            var query = viewModel.MapOne<GetBrandsNamesQuery>();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess || result.Data == null)
            {
                return EndPointResponse<IEnumerable<GetBrandsNamesResponseViewModel>>.Failure(result.ErrorCode);
            }

            var response = result.Data.MapList<GetBrandsNamesResponseViewModel>();

            return EndPointResponse<IEnumerable<GetBrandsNamesResponseViewModel>>.Success(response, "Get Brands Names successfully.");
        }
    }
}
