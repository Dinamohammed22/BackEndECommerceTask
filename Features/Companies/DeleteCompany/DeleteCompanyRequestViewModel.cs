using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Users.DeleteUser.Commands;
using KOG.ECommerce.Features.Companies.Commands;
using KOG.ECommerce.Features.Companies.DeleteCompany.Command;
using KOG.ECommerce.Features.Companies.DeleteCompany.Orchestrator;

namespace KOG.ECommerce.Features.Companies.DeleteCompany
{
    public record DeleteCompanyRequestViewModel(string Id);
    public class DeleteCompanyEndPointRequestValidator : AbstractValidator<DeleteCompanyRequestViewModel>
    {
        public DeleteCompanyEndPointRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("ID is required.").Length(1, 200).WithMessage("ID must be between 1 and 200 characters.");
            ;


        }
    }

    public class DeleteCompanyEndPointRequestProfile : Profile
    {
        public DeleteCompanyEndPointRequestProfile()
        {
            CreateMap<DeleteCompanyRequestViewModel, DeleteCompanyOrchestrator>();
            CreateMap<DeleteCompanyOrchestrator, DeleteUserCommand>();
            CreateMap<DeleteCompanyOrchestrator, DeleteCompanyCommand>();

        }
    }
}