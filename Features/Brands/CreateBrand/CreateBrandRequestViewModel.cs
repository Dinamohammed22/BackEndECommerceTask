using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.CreateGovernorate;
using KOG.ECommerce.Features.Brands.CreateBrand.Commands;
using KOG.ECommerce.Features.Brands.CreateBrand.Orchestrators;

namespace KOG.ECommerce.Features.Brands.CreateBrand
{
    public record CreateBrandRequestViewModel(string Name, List<string> Tags, List<string>? Paths, bool IsActive);

    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequestViewModel>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().Length(2, 200);
        }
    }
    public class CreateBrandEndPointRequestProfile : Profile
    {
        public CreateBrandEndPointRequestProfile()
        {
            CreateMap<CreateBrandRequestViewModel, CreateBrandOrchestrator>();
            CreateMap<CreateBrandOrchestrator, CreateBrandCommand>();
        }
    }

}
