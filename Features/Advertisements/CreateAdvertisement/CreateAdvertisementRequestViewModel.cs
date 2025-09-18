using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Commands;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Advertisements.CreateAdvertisement.Orchestrators;

namespace KOG.ECommerce.Features.Advertisements.CreateAdvertisement
{
    public record CreateAdvertisementRequestViewModel(
        string Title ,
        bool IsActive,
        ImageType ImageTypes,
        List<string> Paths,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate
    );
    public class CreateAdvertisementRequestValidator : AbstractValidator<CreateAdvertisementRequestViewModel>
    {
        public CreateAdvertisementRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            RuleFor(x => x.ImageTypes)
                .IsInEnum().WithMessage("Invalid image type.");

            RuleForEach(x => x.Paths)
                .NotEmpty().WithMessage("Each path must not be empty.");

            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Start date must be in the future.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");
        }
    }

    public class CreateAdvertisementRequestProfile : Profile
    {
        public CreateAdvertisementRequestProfile()
        {
            CreateMap<CreateAdvertisementRequestViewModel, CreateAdvertisementOrchestrator>();
            CreateMap<CreateAdvertisementOrchestrator, CreateAdvertisementCommand>();
        }
    }
}
