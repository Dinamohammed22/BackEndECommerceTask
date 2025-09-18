using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.AddClientImage.Command;

namespace KOG.ECommerce.Features.Clients.AddClientImage
{
    public record AddClientImageRequestViewModel(List<string> Paths);
    public class AddClientImageRequestValidator : AbstractValidator<AddClientImageRequestViewModel>
    {
        public AddClientImageRequestValidator()
        {
        }
    }
    public class AddClientImageRequestProfile : Profile
    {
        public AddClientImageRequestProfile()
        {
            CreateMap<AddClientImageRequestViewModel, AddClientImageCommand>();
        }
    }
}
