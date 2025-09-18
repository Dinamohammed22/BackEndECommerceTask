using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.EditAdvertisement.Commands;
using KOG.ECommerce.Features.Advertisements.EditAdvertisement.Orchestrators;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Advertisements.EditAdvertisement
{
    public record EditAdvertisementRequestViewModel(string ID,string Title,
        bool IsActive,
        ImageType ImageTypes,
        List<string>? Paths,
        string? Hyperlink,
        DateTime StartDate,
        DateTime EndDate
        );
    public class EditAdvertisementRequestValidator : AbstractValidator<EditAdvertisementRequestViewModel>
    {
        public EditAdvertisementRequestValidator() {
            RuleFor(x => x.ID)
                   .NotEmpty().WithMessage("Title is required.");

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
        private bool BeAValidUrl(string hyperlink)
        {
            return Uri.TryCreate(hyperlink, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
    public class EditAdvertisementRequestProfile : Profile
    {
        public EditAdvertisementRequestProfile() {
            CreateMap<EditAdvertisementRequestViewModel, EditAdvertisementOrchestrtor>();
            CreateMap<EditAdvertisementOrchestrtor, EditAdvertisementCommand>();

        }
    }
}
