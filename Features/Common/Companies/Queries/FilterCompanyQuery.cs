using AutoMapper;
using AutoMapper.QueryableExtensions;
using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Companies.CompanyFilterIndex;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Companies;
using Microsoft.EntityFrameworkCore;
using PredicateExtensions;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record FilterCompanyQuery(
        int pageIndex = 1,
        int pageSize = 100,
        string? Name = null,
        string? Mobile = null,
        string? Address = null,
        string? GovernorateId = null,
        string? CityId = null,
        string? Activity = null,
        string? TaxCardID = null,
        string? TaxRegistryNumber = null,
        string? NID = null,
        string? ManagerName = null,
        string? ManagerMobile = null,
        string? ClassificationId = null,
        string? Email = null
    ) : IRequestBase<PagingViewModel<FilterCompanyProfileDTO>>;

    public class GetFilteredCompaniesQueryHandler : RequestHandlerBase<Company, FilterCompanyQuery, PagingViewModel<FilterCompanyProfileDTO>>
    {
        public GetFilteredCompaniesQueryHandler(RequestHandlerBaseParameters<Company> parameters) : base(parameters)
        {
        }


        public override async Task<RequestResult<PagingViewModel<FilterCompanyProfileDTO>>> Handle(FilterCompanyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Build predicate dynamically
                var predicate = PredicateExtensions.PredicateExtensions.Begin<Company>(true);

                predicate = predicate
                    .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name))
                    .And(c => string.IsNullOrEmpty(request.Mobile) || c.Mobile.Contains(request.Mobile))
                    .And(c => string.IsNullOrEmpty(request.Address) || c.Address.Contains(request.Address))
                    .And(c => string.IsNullOrEmpty(request.GovernorateId) || c.GovernorateId == request.GovernorateId)
                    .And(c => string.IsNullOrEmpty(request.CityId) || c.CityId == request.CityId)
                    .And(c => string.IsNullOrEmpty(request.Activity) || c.Activity.Contains(request.Activity))
                    .And(c => string.IsNullOrEmpty(request.TaxCardID) || c.TaxCardID == request.TaxCardID)
                    .And(c => string.IsNullOrEmpty(request.TaxRegistryNumber) || c.TaxRegistryNumber == request.TaxRegistryNumber)
                    .And(c => string.IsNullOrEmpty(request.NID) || c.NID == request.NID)
                    .And(c => string.IsNullOrEmpty(request.ManagerName) || c.ManagerName.Contains(request.ManagerName))
                    .And(c => string.IsNullOrEmpty(request.ManagerMobile) || c.ManagerMobile.Contains(request.ManagerMobile))
                    .And(c => string.IsNullOrEmpty(request.ClassificationId) || c.ClassificationId == request.ClassificationId)
                    .And(c => string.IsNullOrEmpty(request.Email) || c.Email.Contains(request.Email));

                // Query the repository
                var query = _repository.Get(predicate)
                    .Include(c => c.City)
                    .Include(c => c.Governorate)
                    .Include(c => c.Classification)
                    .Map<FilterCompanyProfileDTO>();

                // Paginate the results
                var pagedResult = await query.ToPagesAsync(request.pageIndex, request.pageSize);

                // Return the paginated results
                return RequestResult<PagingViewModel<FilterCompanyProfileDTO>>.Success(pagedResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return RequestResult<PagingViewModel<FilterCompanyProfileDTO>>.Failure(ErrorCode.NotFound);
            }
        }
    } 
        }

    