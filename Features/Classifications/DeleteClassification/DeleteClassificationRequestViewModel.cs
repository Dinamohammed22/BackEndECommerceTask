using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Classifications.DeleteClassification.Commands;
using KOG.ECommerce.Features.Common.Companies.Queries;


namespace KOG.ECommerce.Features.Classifications.DeleteClassification;

public record DeleteClassificationRequestViewModel(string ID);
public class DeleteClassificationRequestValidator : AbstractValidator<DeleteClassificationRequestViewModel>
{
    public DeleteClassificationRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}
public class DeleteClassificationEndPointRequestProfile : Profile
{
    public DeleteClassificationEndPointRequestProfile()
    {
        CreateMap<DeleteClassificationRequestViewModel, DeleteClassificationCommand>();
        CreateMap<DeleteClassificationCommand,CheckCompanyHasClassificationQuery>();
    }
}

