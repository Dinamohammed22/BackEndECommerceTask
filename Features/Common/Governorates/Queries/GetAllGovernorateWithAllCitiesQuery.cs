using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Cities.DTOs;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Cities;
using KOG.ECommerce.Models.Governorates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KOG.ECommerce.Features.Common.Governorates.Queries
{
    public record GetAllGovernorateWithAllCitiesQuery(string? Name=null,int pageIndex = 1, int pageSize = 100) : IRequestBase<PagingViewModel<GovernorateWithAllCitiesProfileDTO>>;
    
        public class GetAllGovernorateWithAllCitiesQueryHandler : RequestHandlerBase<Governorate, GetAllGovernorateWithAllCitiesQuery, PagingViewModel<GovernorateWithAllCitiesProfileDTO>>
        {
            public GetAllGovernorateWithAllCitiesQueryHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
            {
            }

            public override async Task<RequestResult<PagingViewModel<GovernorateWithAllCitiesProfileDTO>>> Handle(GetAllGovernorateWithAllCitiesQuery request, CancellationToken cancellationToken)
            {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<Governorate>(true);

            predicate = predicate
            .And(c => string.IsNullOrEmpty(request.Name) || c.Name.Contains(request.Name));
                var governorates = await _repository
                    .Get(predicate)
                    .Select(g=>new GovernorateWithAllCitiesProfileDTO
                    {
                        ID = g.ID,
                        Name = g.Name,
                         GovernorateCode=g.GovernorateCode,
                          IsActive = g.IsActive,    
                        Cities = g.Cities.Where(c=>!c.Deleted).Select(c=> new CitiesForGovernorateProfileDTO
                        {
                            Name= c.Name,
                        }).ToList(),
                    })
                    .ToPagesAsync(request.pageIndex, request.pageSize);

                return RequestResult<PagingViewModel<GovernorateWithAllCitiesProfileDTO>>.Success(governorates);
            }
        }
    
}
