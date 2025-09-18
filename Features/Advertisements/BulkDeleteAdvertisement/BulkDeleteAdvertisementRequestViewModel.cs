using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Advertisements.BulkDeleteAdvertisement.Orchisterators;

namespace KOG.ECommerce.Features.Advertisements.BulkDeleteAdvertisement
{
    public record BulkDeleteAdvertisementRequestViewModel(List<string> IDs);
    public class BulkDeleteAdvertisementRequestValidator : AbstractValidator<BulkDeleteAdvertisementRequestViewModel>
    {
        public BulkDeleteAdvertisementRequestValidator()
        {
        }
    }
    public class BulkDeleteAdvertisementRequestProfile : Profile
    {
        public BulkDeleteAdvertisementRequestProfile()
        {
            CreateMap<BulkDeleteAdvertisementRequestViewModel, BulkDeleteAdvertisementOrchisterator>();
        }
    }
}
