using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.BulkActivateAdvertisement.Orchisterators;

namespace KOG.ECommerce.Features.Advertisements.BulkActivateAdvertisement
{
    public record BulkActivateAdvertisementRequestViewModel(List<string> IDs);
    public class BulkActivateAdvertisementRequestValidator : AbstractValidator<BulkActivateAdvertisementRequestViewModel>
    {
        public BulkActivateAdvertisementRequestValidator()
        {
        }
    }
    public class BulkActivateAdvertisementProfile : Profile
    {
        public BulkActivateAdvertisementProfile()
        {
            CreateMap<BulkActivateAdvertisementRequestViewModel, BulkActivateAdvertisementOrchisterator>();
        }
    }
}
