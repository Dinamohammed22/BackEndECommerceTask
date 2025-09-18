using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.BulkDeactivateAdvertisement.Orchisterators;

namespace KOG.ECommerce.Features.Advertisements.BulkDeactivateAdvertisement
{
    public record BulkDeactivateAdvertisementRequestViewModel(List<string> IDs);
    public class BulkDeactivateAdvertisementRequestValidator : AbstractValidator<BulkDeactivateAdvertisementRequestViewModel>
    {
        public BulkDeactivateAdvertisementRequestValidator()
        {
        }
    }
    public class BulkDeactivateAdvertisementRequestProfile : Profile
    {
        public BulkDeactivateAdvertisementRequestProfile()
        {
            CreateMap<BulkDeactivateAdvertisementRequestViewModel, BulkDeactivateAdvertisementOrchisterator>();
        }
    }
}
