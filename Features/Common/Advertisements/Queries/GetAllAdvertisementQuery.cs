using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Common.Advertisements.Queries
{
    public record GetAllAdvertisementQuery(int pageIndex = 1, int pageSize = 100):IRequestBase<PagingViewModel<GetAllAdvertisementDTO>>;
    public class GetAllAdvertisementQueryHandler : RequestHandlerBase<Advertisement, GetAllAdvertisementQuery, PagingViewModel<GetAllAdvertisementDTO>>
    {
        public GetAllAdvertisementQueryHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<PagingViewModel<GetAllAdvertisementDTO>>> Handle(GetAllAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.Get()
              .Map<GetAllAdvertisementDTO>()
              .ToPagesAsync(request.pageIndex, request.pageSize);
            foreach (var dto in query.Items)
            {

                var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(dto.ID, SourceType.Advertisement));
                dto.Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty;


            }
            return RequestResult<PagingViewModel<GetAllAdvertisementDTO>>.Success(query);
        }
    }
}
