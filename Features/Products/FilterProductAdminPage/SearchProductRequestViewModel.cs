using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Products.FilterProductAdminPage
{
    public record SearchProductRequestViewModel(string? ProductName,
                                      string? CategoryId,
                                      string? SubcategoryId,
                                      bool? IsActive,
                                      string? CompanyId,
                                      DateTime? From,
                                      DateTime? To,
                                      int pageIndex = 1, int pageSize = 100
                                      );

    public class SearchProductRequestValidator : AbstractValidator<SearchProductRequestViewModel>
    {
        public SearchProductRequestValidator()
        {

        }
    }

    public class SearchProductRequestProfile : Profile
    {
        public SearchProductRequestProfile()
        {
            CreateMap<SearchProductRequestViewModel, SearchProductQuery>();
        }
    }

}
