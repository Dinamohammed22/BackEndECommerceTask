using AutoMapper;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.GetListGovernorate;

namespace KOG.ECommerce.Features.Governorates.GetDropdownListGovernorate
{
    public record GetDropdownListGovernorateResponseViewModel(string Name,string ID);
    public class GetDropdownListGovernorateResponseProfile : Profile
    {
        public GetDropdownListGovernorateResponseProfile()
        {
            CreateMap<SelectListItemViewModel, GetDropdownListGovernorateResponseViewModel>();
        }
    }
}
