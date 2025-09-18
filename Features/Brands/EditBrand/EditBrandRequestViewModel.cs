using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.UpdateGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.UpdateGovernorate;
using KOG.ECommerce.Features.Brands.EditBrand.Commands;
using KOG.ECommerce.Features.Brands.EditBrand.Orchestrators;

namespace KOG.ECommerce.Features.Brands.EditBrand
{
    public record EditBrandRequestViewModel(string ID, string Name, List<string> Tags, List<string>? Paths, bool IsActive);

    public class EditBrandRequestValidator : AbstractValidator<EditBrandRequestViewModel>
    {
        public EditBrandRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().Length(2, 200);
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }

    }
    public class EditBrandEndPointRequestProfile : Profile
    {
        public EditBrandEndPointRequestProfile()
        {
            CreateMap<EditBrandRequestViewModel, EditBrandOrchestrator>();
            CreateMap<EditBrandOrchestrator, EditBrandCommand>();
        }
    }

}
