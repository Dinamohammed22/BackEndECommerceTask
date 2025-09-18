using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Classifications.CreateClassification.Commands;


namespace KOG.ECommerce.Features.Classifications.CreateClassification;

public record CreateClassificationRequestViewModel(string Name);
public class CreateClassificationRequestValidator : AbstractValidator<CreateClassificationRequestViewModel>
{
    public CreateClassificationRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty().Length(2, 200);
    }
}
public class CreateClassificationEndPointRequestProfile : Profile
{
    public CreateClassificationEndPointRequestProfile()
    {
        CreateMap<CreateClassificationRequestViewModel, CreateClassificationCommand>();


    }
}