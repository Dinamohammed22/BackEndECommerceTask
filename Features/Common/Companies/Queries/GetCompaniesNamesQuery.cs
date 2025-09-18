using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Models.Companies;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Features.Common.Companies.Queries
{
    public record GetCompaniesNamesQuery(List<string>? ClassificationId) : IRequestBase<IEnumerable<GetCompaniesNamesDTO>>;
    public class GetCompaniesNamesQueryHandler : RequestHandlerBase<Company, GetCompaniesNamesQuery, IEnumerable<GetCompaniesNamesDTO>>
    {
        public GetCompaniesNamesQueryHandler(RequestHandlerBaseParameters<Company> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<IEnumerable<GetCompaniesNamesDTO>>> Handle(GetCompaniesNamesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Company> query = _repository.Get(c => c.IsActive)
                                .Include(c => c.products);

            if (request.ClassificationId != null && request.ClassificationId.Any())
            {
                query = query.Where(c => c.ClassificationId != null && request.ClassificationId.Contains(c.ClassificationId));
            }



            var companies = await query.ToListAsync(cancellationToken);
            var result = new List<GetCompaniesNamesDTO>();

            foreach (var company in companies)
            {
                int totalProductCount = 0;

                if (request.ClassificationId != null && request.ClassificationId.Any())
                {
                    foreach (var classificationId in request.ClassificationId)
                    {
                        var productCount = await _mediator.Send(new GetProductCountByCompanyIdQuery(company.ID, classificationId));
                        if (productCount.Data > 0)
                        {
                            totalProductCount = productCount.Data;
                            break; // Once a matching classification is found, skip the rest
                        }
                    }
                }
                else
                {
                    var productCount = await _mediator.Send(new GetProductCountByCompanyIdQuery(company.ID, null));
                    if (productCount?.Data > 0)
                    {
                        totalProductCount = productCount.Data;
                    }
                }

                result.Add(new GetCompaniesNamesDTO(company.ID, company.Name, totalProductCount));
            }

            return RequestResult<IEnumerable<GetCompaniesNamesDTO>>.Success(result);
        }

    }

}
