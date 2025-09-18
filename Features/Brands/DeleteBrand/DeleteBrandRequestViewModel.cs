using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate;
using KOG.ECommerce.Features.Brands.DeleteBrand.Commands;
using KOG.ECommerce.Features.Common.Products.Queries;

namespace KOG.ECommerce.Features.Brands.DeleteBrand
{
    public record DeleteBrandRequestViewModel(string ID);

    public class DeleteBrandRequestValidator : AbstractValidator<DeleteBrandRequestViewModel>
    {
        public DeleteBrandRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class DeleteBrandEndPointRequestProfile : Profile
    {
        public DeleteBrandEndPointRequestProfile()
        {
            CreateMap<DeleteBrandRequestViewModel, DeleteBrandCommand>();
            CreateMap<DeleteBrandCommand, CheckIfProductHasBrandQuery>();
        }
    }

}
