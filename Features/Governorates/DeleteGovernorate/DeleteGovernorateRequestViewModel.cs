using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.DeleteCity.Commands;
using KOG.ECommerce.Features.Common.Cities.Queries;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Commands;
using KOG.ECommerce.Features.Governorates.DeleteGovernorate.Orchestrators;

namespace KOG.ECommerce.Features.Governorates.DeleteGovernorate;

public record DeleteGovernorateRequestViewModel(string ID);

public class DeleteGovernorateRequestValidator : AbstractValidator<DeleteGovernorateRequestViewModel>
{
    public DeleteGovernorateRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}
public class DeleteGovernorateEndPointRequestProfile : Profile
{
    public DeleteGovernorateEndPointRequestProfile()
    {
        CreateMap<DeleteGovernorateRequestViewModel, DeleteGovernorateOrchestrator>();
        CreateMap<DeleteGovernorateCommand, CheckCompanyGovernorateIdQuery>();
        CreateMap<DeleteGovernorateCommand, CheckShippingAddressGovernorateIdQuery>();
        CreateMap< DeleteGovernorateOrchestrator, DeleteGovernorateCommand>();
        CreateMap<DeleteGovernorateOrchestrator, GetCitiesIdsByGovernorateIDQuery>();

    }
}