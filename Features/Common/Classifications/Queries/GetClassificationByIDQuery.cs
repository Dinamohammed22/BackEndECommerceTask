using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Classifications.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Common.Classifications.Queries
{
    public record GetClassificationByIDQuery(string ID):IRequestBase<GetClassificationByIDDTO>;
    public class GetClassificationByIDQueryHandler : RequestHandlerBase<Classification, GetClassificationByIDQuery, GetClassificationByIDDTO>
    {
        public GetClassificationByIDQueryHandler(RequestHandlerBaseParameters<Classification> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<GetClassificationByIDDTO>> Handle(GetClassificationByIDQuery request, CancellationToken cancellationToken)
        {
           var classification= _repository.GetByID(request.ID).MapOne<GetClassificationByIDDTO>();
            if (classification == null)
            {
                return RequestResult<GetClassificationByIDDTO>.Failure(ErrorCode.NotFound);
            }
            return RequestResult<GetClassificationByIDDTO>.Success(classification);
        }
    }
}