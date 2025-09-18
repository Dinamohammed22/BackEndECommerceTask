using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.UpdateGovernorate.Commands;

namespace KOG.ECommerce.Features.Governorates.UpdateGovernorate;

public record UpdateGovernorateRequestViewModel(string ID, string Name, string GovernorateCode, bool IsActive);
public class UpdateGovernorateRequestValidator : AbstractValidator<UpdateGovernorateRequestViewModel>
{
    public UpdateGovernorateRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().Length(2, 200);
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }

}
public class UpdateGovernorateEndPointRequestProfile : Profile
{
    public UpdateGovernorateEndPointRequestProfile()
    {
        CreateMap<UpdateGovernorateRequestViewModel, UpdateGovernorateCommand>();
    }
}