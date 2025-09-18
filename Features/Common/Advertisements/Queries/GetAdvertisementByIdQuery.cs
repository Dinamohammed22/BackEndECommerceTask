using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Advertisements.DTOs;
using KOG.ECommerce.Features.Common.Brands.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Advertisements;
using KOG.ECommerce.Models.Brands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Common.Advertisements.Queries
{
    public record GetAdvertisementByIdQuery(string ID) : IRequestBase<GetAdvertisementByIdDTO>;
    public class GetAdvertisementByIdQueryHandler : RequestHandlerBase<Advertisement, GetAdvertisementByIdQuery, GetAdvertisementByIdDTO>
    {
        public GetAdvertisementByIdQueryHandler(RequestHandlerBaseParameters<Advertisement> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetAdvertisementByIdDTO>> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            var advertisement = _repository.GetByID(request.ID).MapOne<GetAdvertisementByIdDTO>();

            if (advertisement == null)
            {
                return RequestResult<GetAdvertisementByIdDTO>.Failure(ErrorCode.NotFound);
            }

            var mediaResult = await _mediator.Send(new GetMediaForAnySourceQuery(request.ID, SourceType.Advertisement));

            GetAdvertisementByIdDTO advertisementWithMedia = advertisement with
            {
                Path = mediaResult.IsSuccess ? mediaResult.Data : string.Empty
            };

            return RequestResult<GetAdvertisementByIdDTO>.Success(advertisementWithMedia);
        }

    }
    
}
