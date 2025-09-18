using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement.Commands;

namespace KOG.ECommerce.Features.Advertisements.DeactivateAdvertisement
{
    public record DeactivateAdvertisementRequestViewModel(string ID);
    public class DeactivateAdvertisementRequestValidtor : AbstractValidator<DeactivateAdvertisementRequestViewModel>
    {
        public DeactivateAdvertisementRequestValidtor()
        {
        }
    }

    public class DeactivateAdvertisementRequestProfile : Profile
    {
        public DeactivateAdvertisementRequestProfile()
        {
            CreateMap<DeactivateAdvertisementRequestViewModel, DeactivateAdvertisementCommand>();
        }
    }
}
