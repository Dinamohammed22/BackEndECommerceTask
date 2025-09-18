using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;

namespace KOG.ECommerce.Features.Cities.DeleteCity
{
    public record DeleteCityRequestViewModel(string Id);
    public class DeleteCityEndPointRequestValidator : AbstractValidator<DeleteCityRequestViewModel>
    {
        public DeleteCityEndPointRequestValidator()
        {
            RuleFor(request => request.Id)
            .NotEmpty().WithMessage("ID is required.")
            .Length(1, 100).WithMessage("ID must be between 1 and 50 characters.");

        }
    }
    public class CreateGroupEndPointRequestProfile : Profile
    {
        public CreateGroupEndPointRequestProfile()
        {
            CreateMap<DeleteCityRequestViewModel, DeleteCityCommand>();
            CreateMap<DeleteCityCommand, CheckCompanyCityIdQuery>();
            CreateMap<DeleteCityCommand, CheckShippingAddressCityIdQuery>();
        }
    }
}
