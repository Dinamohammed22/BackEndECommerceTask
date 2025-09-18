using KOG.ECommerce.Common.Endpoints;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Discounts.DTOs;
using KOG.ECommerce.Features.Common.Discounts.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;

namespace KOG.ECommerce.Features.Discounts.GetAllDiscounts
{
    public class GetAllDiscountsEndpoint : EndpointBase<GetAllDiscountsRequestViewModel, GetAllDiscountsResponseViewModel>
    {
        public GetAllDiscountsEndpoint(EndpointBaseParameters<GetAllDiscountsRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }
        [HttpGet]
        [TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.GetAllDiscounts })]
        public async Task<EndPointResponse<PagingViewModel<GetAllDiscountsResponseViewModel>>> GetList([FromQuery]GetAllDiscountsRequestViewModel viewModel)
        {


            var result = await _mediator.Send(viewModel.MapOne<GetAllDiscountsQuery>());

              //var response = result.Data.MapList<GetAllDiscountsResponseViewModel>();
              var response =result.Data.MapPage<GetAllDiscountsDTO, GetAllDiscountsResponseViewModel>();

            if (result.IsSuccess && result.Data != null)
                return EndPointResponse<PagingViewModel<GetAllDiscountsResponseViewModel>>.Success(response, " Get All Discounts successfully.");
            else
                return EndPointResponse<PagingViewModel<GetAllDiscountsResponseViewModel>>.Failure(result.ErrorCode);

        }

    }
}
