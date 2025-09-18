using AutoMapper;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Common.Classifications.DTOs
{
    public record GetClassificationByIDDTO(string ID,string Name);
    public class GetClassificationByIDProfile : Profile
    {
        public GetClassificationByIDProfile()
        {
            CreateMap<Classification, GetClassificationByIDDTO>();
        }
    }
}
