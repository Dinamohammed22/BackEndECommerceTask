using AutoMapper;
using KOG.ECommerce.Models.Medias;

namespace KOG.ECommerce.Features.Common.Medias.DTOs
{
    public record MediaDTO(string ID,string Path);
    public class MediaDTOProfile : Profile
    {
        public MediaDTOProfile() { 
            CreateMap<Media, MediaDTO>();
        }
    }

}
