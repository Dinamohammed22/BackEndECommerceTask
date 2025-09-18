using AutoMapper;
using KOG.ECommerce.Features.Common.Classifications.DTOs;

namespace KOG.ECommerce.Features.Classifications.GetClassificationByID
{
    public record GetClassificationByIDResponseViewModel(string ID, string Name);
    public class GetClassificationByIDResponseProfile : Profile
    {
        public GetClassificationByIDResponseProfile()
        {
            CreateMap<GetClassificationByIDDTO, GetClassificationByIDResponseViewModel>();
        }
    }
}
