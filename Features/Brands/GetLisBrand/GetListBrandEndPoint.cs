using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.GetLisBrand
{
    public class GetListBrandEndPoint : EndpointBase<GetListBrandRequestViewModel, GetListBrandResponseViewModel>
    {
        public GetListBrandEndPoint(EndpointBaseParameters<GetListBrandRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetBrandlist })]
        public async Task<EndPointResponse<PagingViewModel<GetListBrandResponseViewModel>>> GetList([FromQuery]GetListBrandRequestViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<GetListBrandQuery>());
            var response = result.Data.MapPage<BrandProfileDTO, GetListBrandResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetListBrandResponseViewModel>>.Success(response, "Get Brands successfully.");
            else
                return EndPointResponse<PagingViewModel<GetListBrandResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
