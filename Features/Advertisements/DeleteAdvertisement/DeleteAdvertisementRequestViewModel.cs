using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.DeleteAdvertisement.Commands;

namespace KOG.ECommerce.Features.Advertisements.DeleteAdvertisement
{
    public record DeleteAdvertisementRequestViewModel(string ID);
    public class DeleteAdvertisementRequestValidator : AbstractValidator<DeleteAdvertisementRequestViewModel>
    {
        public DeleteAdvertisementRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class DeleteAdvertisementRequestProfile : Profile
    {
        public DeleteAdvertisementRequestProfile()
        {
            CreateMap<DeleteAdvertisementRequestViewModel, DeleteAdvertisementCommand>();
        }
    }
}
