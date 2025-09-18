using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Pages.CreatePage.Commands;
using KOG.ECommerce.Features.Pages.CreatePage;
using KOG.ECommerce.Features.Modules.CreateModule.Commands;

namespace KOG.ECommerce.Features.Modules.CreateModule
{
    public record CreateModuleRequestViewModel(string Name, string PageId);
    public class CreateModuleRequestValidator : AbstractValidator<CreateModuleRequestViewModel>
    {
        public CreateModuleRequestValidator()
        {
            RuleFor(request => request.Name)
                           .NotEmpty().WithMessage("Name is required.");

        }
    }

    public class CreateModuleEndPointProfile : Profile
    {
        public CreateModuleEndPointProfile()
        {
            CreateMap<CreateModuleRequestViewModel, CreateModuleCommand>();
        }
    }

}
