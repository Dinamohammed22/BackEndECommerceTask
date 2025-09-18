using AutoMapper;
using KOG.ECommerce.Common.Views;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Users.GetVerifyStatusList
{
    public record GetVerifyStatusListResponseViewModel(string Name, string ID);

    public class GetVerifyStatusListResponseProfile : Profile
    {
        public GetVerifyStatusListResponseProfile()
        {
            CreateMap<SelectListItemViewModel, GetVerifyStatusListResponseViewModel>();
        }
    }
    }
