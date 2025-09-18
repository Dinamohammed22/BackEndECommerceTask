using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Features.Brands.GetBrandsNames;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Companies.GetCompaniesNames
{
    public class GetCompaniesNamesEndPoint : EndpointBase<GetCompaniesNamesRequestViewModel, GetCompaniesNamesResponseViewModel>
    {
        public GetCompaniesNamesEndPoint(EndpointBaseParameters<GetCompaniesNamesRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetCompaniesNames })]
        public async Task<EndPointResponse<IEnumerable<GetCompaniesNamesResponseViewModel>>> GetCompaniesNames([FromQuery] GetCompaniesNamesRequestViewModel viewModel)
        {
            var query = viewModel.MapOne<GetCompaniesNamesQuery>();
            var result = await _mediator.Send(query);

            if (!result.IsSuccess || result.Data == null)
            {
                return EndPointResponse<IEnumerable<GetCompaniesNamesResponseViewModel>>.Failure(result.ErrorCode);
            }

            var response = result.Data.MapList<GetCompaniesNamesResponseViewModel>();

            return EndPointResponse<IEnumerable<GetCompaniesNamesResponseViewModel>>.Success(response, "Get Companies Names successfully.");
        }
    }
}
