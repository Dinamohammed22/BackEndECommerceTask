using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Common.Views;

namespace KOG.ECommerce.Features.Companies.SelectListCompany
{
    public record SelectListCompanyResponseViewModel(string Name, string ID);
    public class SelectListCompanyResponseProfile : Profile
    {
        public SelectListCompanyResponseProfile()
        {
            CreateMap<SelectListItemViewModel, SelectListCompanyResponseViewModel>();
        }
    }
}
