using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.CompanyGovernorates.DTOs;
using KOG.ECommerce.Features.Common.Medias.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using KOG.ECommerce.Models.Enums;
using Microsoft.EntityFrameworkCore;
namespace KOG.ECommerce.Features.Common.Companies.Queries;
public record GetCompanyByIDQuery(string ID) : IRequestBase<CompanyProfileDTO>;

public class GetCityIdByUIDQueryHandler : RequestHandlerBase<Company, GetCompanyByIDQuery, CompanyProfileDTO>
{
    public GetCityIdByUIDQueryHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters) { }

    public override async Task<RequestResult<CompanyProfileDTO>> Handle(GetCompanyByIDQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository
    .Get(c => c.ID == request.ID)
    .Include(c => c.Governorate)
    .Include(c => c.City)
    .Include(c => c.Classification)
    .Include(c => c.CompanyGovernorates)!
        .ThenInclude(cg => cg.Governorate)
    .FirstOrDefaultAsync();

        if (entity == null)
            return RequestResult<CompanyProfileDTO>.Failure(ErrorCode.NotFound);

        // Manual mapping from Company to CompanyProfileDTO
        var dto = new CompanyProfileDTO
        {
            ID = entity.ID,
            Email=entity.Email,
            Name = entity.Name,
            Mobile = entity.Mobile,
            Address = entity.Address,
            GovernorateId = entity.GovernorateId,
            GovernorateName = entity.Governorate?.Name ?? string.Empty,
            CityId = entity.CityId,
            CityName = entity.City?.Name ?? string.Empty,
            TaxCardID = entity.TaxCardID,
            TaxRegistryNumber = entity.TaxRegistryNumber,
            NID = entity.NID,
            ManagerName = entity.ManagerName,
            ManagerMobile = entity.ManagerMobile,
            ClassificationId = entity.ClassificationId,
            ClassificationName = entity.Classification?.Name ?? string.Empty,
            MinimumQuantity = entity.MinimumQuantity,
            IsActive = entity.IsActive,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            CreditLimit = entity.CreditLimit,
            GovernorateIds = entity.CompanyGovernorates!
           .Where(cg => !cg.Deleted)
           .Select(cg => new CompanyGovernorateDTO(
               cg.GovernorateId,
               cg.Governorate?.Name ?? string.Empty
           ))
           .ToList()
        };
        var CompanyFiles = await _mediator.Send(new GetAllMediaForAnySourceQuery(request.ID, SourceType.CompanyFiles));
        var CompanyImage = await _mediator.Send(new GetAllMediaForAnySourceQuery(request.ID, SourceType.CompanyImage));


        dto.CompanyFiles = CompanyFiles.IsSuccess ? CompanyFiles.Data : null!;
        dto.CompanyImage = CompanyImage.IsSuccess ? CompanyImage.Data : null;
        
        return RequestResult<CompanyProfileDTO>.Success(dto);
    }

}
