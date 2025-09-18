using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.ActiveAdvertisement.Commands;

namespace KOG.ECommerce.Features.Advertisements.ActiveAdvertisement
{
    public record ActiveAdvertisementRequestViewModel(string ID);
    public class ActiveAdvertisementRequestValidator : AbstractValidator<ActiveAdvertisementRequestViewModel>
    {
        public ActiveAdvertisementRequestValidator()
        {
        }
    }
    public class ActiveAdvertisementRequestProfile : Profile
    {
        public ActiveAdvertisementRequestProfile()
        {
            CreateMap<ActiveAdvertisementRequestViewModel, ActiveAdvertisementCommand>();
        }
    }
}
