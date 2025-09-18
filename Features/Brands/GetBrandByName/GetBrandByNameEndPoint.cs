using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Brands.GetBrandByName
{
    public class GetBrandByNameEndPoint : EndpointBase<GetBrandByNameRequestViewModel, GetBrandByNameResponseViewModel>
    {
        public GetBrandByNameEndPoint(EndpointBaseParameters<GetBrandByNameRequestViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetBrandByName })]
        public async Task<EndPointResponse<PagingViewModel<GetBrandByNameResponseViewModel>>> GetByName([FromQuery] GetBrandByNameRequestViewModel viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<GetBrandByNameQuery>());
            var response = result.Data.MapPage<BrandProfileDTO, GetBrandByNameResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetBrandByNameResponseViewModel>>.Success(response, "Brands Filtered Successfully");
            else
                return EndPointResponse<PagingViewModel<GetBrandByNameResponseViewModel>>.Failure(result.ErrorCode);

        }
    }
}
