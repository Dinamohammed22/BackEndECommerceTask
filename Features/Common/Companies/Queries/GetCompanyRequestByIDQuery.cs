using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record GetCompanyRequestByIDQuery(string ID) : IRequestBase<GetCompanyRequestByIdDTO>;

    public class GetCompanyRequestByIDQueryHandler : RequestHandlerBase<Company, GetCompanyRequestByIDQuery, GetCompanyRequestByIdDTO>
    {
        public GetCompanyRequestByIDQueryHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

        public override async Task<RequestResult<GetCompanyRequestByIdDTO>> Handle(GetCompanyRequestByIDQuery request, CancellationToken cancellationToken)
        {
            var company = await _repository
                .Get(c => c.ID == request.ID)
                .Include(c => c.User)
                .Include(c => c.Governorate)
                .Include(c => c.City)
                .Include(c => c.Classification)
                .Map<GetCompanyRequestByIdDTO>()
                .FirstOrDefaultAsync();

            if (company == null)
                return RequestResult<GetCompanyRequestByIdDTO>.Failure(ErrorCode.NotFound);

            var CompanyFiles = await _mediator.Send(new GetAllMediaForAnySourceQuery(request.ID, SourceType.CompanyFiles));
            var CompanyImage = await _mediator.Send(new GetAllMediaForAnySourceQuery(request.ID, SourceType.CompanyImage));

            GetCompanyRequestByIdDTO CompanyDTO = company with
            {
                CompanyFiles = CompanyFiles.IsSuccess ? CompanyFiles.Data : null !,
                CompanyImage = CompanyImage.IsSuccess ? CompanyImage.Data : null !
            };

            return RequestResult<GetCompanyRequestByIdDTO>.Success(CompanyDTO);
        }
    }
}
