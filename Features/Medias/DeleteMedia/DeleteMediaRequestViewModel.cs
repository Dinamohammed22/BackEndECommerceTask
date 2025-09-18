using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Medias.DeleteMedia.Commands;

namespace KOG.ECommerce.Features.Medias.DeleteMedia
{
    public record DeleteMediaRequestViewModel(string ID);

    public class DeleteMediaRequestValidator : AbstractValidator<DeleteMediaRequestViewModel>
    {
        public DeleteMediaRequestValidator() { }
    }
    public class DeleteMediaRequestProfile : Profile
    {
        public DeleteMediaRequestProfile()
        {
            CreateMap<DeleteMediaRequestViewModel, DeleteMediaCommand>();
        }
    }
}
