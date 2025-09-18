using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Medias.SaveMedia
{
    public record SaveMediaRequestViewModel(string SourceId, SourceType SourceType, List<string> Paths);
    public class SaveMediaRequestValidator:AbstractValidator<SaveMediaRequestViewModel>
    {
        public SaveMediaRequestValidator() { }
    }
    public class SaveMediaRequestProfile : Profile
    {
        public SaveMediaRequestProfile()
        {
            CreateMap<SaveMediaRequestViewModel, SaveMediaCommand>();
        }
    }
}
