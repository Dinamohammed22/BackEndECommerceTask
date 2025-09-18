using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Classifications.UpdateClassification.Commands;

namespace KOG.ECommerce.Features.Classifications.UpdateClassification;

public record UpdateClassificationRequestViewModel(string ID, string Name);

public class UpdateClassificationRequestValidator : AbstractValidator<UpdateClassificationRequestViewModel>
{
    public UpdateClassificationRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().Length(2, 200);
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }

}
public class UpdateClassificationEndPointRequestProfile : Profile
{
    public UpdateClassificationEndPointRequestProfile()
    {
        CreateMap<UpdateClassificationRequestViewModel, UpdateClassificationCommand>();
    }
}

