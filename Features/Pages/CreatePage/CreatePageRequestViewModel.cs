using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Pages.CreatePage.Commands;

namespace KOG.ECommerce.Features.Pages.CreatePage
{
    public record CreatePageRequestViewModel(string Name);
    public class CreatePageRequestValidator : AbstractValidator<CreatePageRequestViewModel>
    {
        public CreatePageRequestValidator()
        {
            RuleFor(request => request.Name)
                           .NotEmpty().WithMessage("Name is required.");
                  
        }
    }

    public class CreatePageEndPointProfile : Profile
    {
        public CreatePageEndPointProfile()
        {
            CreateMap<CreatePageRequestViewModel, CreatePageCommand>();
        }
    }
}
